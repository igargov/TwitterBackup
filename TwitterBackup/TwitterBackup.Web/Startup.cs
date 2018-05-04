using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterBackup.Data;
using TwitterBackup.Data.Models.Identity;
using AutoMapper;
using TwitterBackup.Providers;
using TwitterBackup.Services;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.TwitterApiClient.RestClientFactory;
using TwitterBackup.TwitterApiClient;
using Microsoft.Extensions.Caching.Memory;
using TwitterBackup.Services.ViewModels;
using System.Threading.Tasks;

namespace TwitterBackup.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<TwitterBackupDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AzureSQL")));
            }
            else
            {
                services.AddDbContext<TwitterBackupDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocalSQL")));
            }

            services.BuildServiceProvider().GetService<TwitterBackupDbContext>().Database.Migrate();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TwitterBackupDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            //services.AddTransient<IEmailSender, EmailSender>();

            services.AddMemoryCache(mc => new MemoryCacheOptions()
            {

            });

            services.AddSingleton<IRestClientFactory, RestClientFactory>();
            services.AddSingleton<IRestRequestFactory, RestRequestFactory>();

            services.AddSingleton<TwitterAccessTokenProvider, TwitterAccessTokenProvider>(tatp =>
            {
                var key = Environment.GetEnvironmentVariable("CONSUMER_KEY", EnvironmentVariableTarget.Machine);
                var secret = Environment.GetEnvironmentVariable("CONSUMER_SECRET", EnvironmentVariableTarget.Machine);

                return new TwitterAccessTokenProvider(key, secret);
            });

            services.AddScoped<ITwitterApiService, TwitterApiService>();
            services.AddScoped<ITwitterAccountService, TwitterAccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddScoped<TwitterAccountViewModel, TwitterAccountViewModel>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper();
            services.AddMvc();
            services.AddAntiforgery(opt => { opt.HeaderName = "token"; });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            this.CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", "User" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(roleName));
                }
            }

            var adminSettings = this.Configuration.GetSection("AdminData");

            string userName = adminSettings.GetValue<string>("UserName");
            string userEmail = adminSettings.GetValue<string>("UserEmail");
            string userPassword = adminSettings.GetValue<string>("UserPassword");

            var admin = new User()
            {
                UserName = userName,
                Email = userEmail
            };

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, userPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}