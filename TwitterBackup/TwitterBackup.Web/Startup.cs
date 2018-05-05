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
using TwitterBackup.Services.Contracts;

namespace TwitterBackup.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string consumerKey;
            string consumerSecret;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<TwitterBackupDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AzureSQL")));

                var section = Configuration.GetSection("TwitterAppSecrets");

                consumerKey = section.GetValue<string>("ConsumerKey");
                consumerSecret = section.GetValue<string>("ConsumerSecret");
            }
            else
            {
                services.AddDbContext<TwitterBackupDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocalSQL")));

                consumerKey = Environment.GetEnvironmentVariable("CONSUMER_KEY", EnvironmentVariableTarget.Machine);
                consumerSecret = Environment.GetEnvironmentVariable("CONSUMER_SECRET", EnvironmentVariableTarget.Machine);
            }

            services.BuildServiceProvider().GetService<TwitterBackupDbContext>().Database.Migrate();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TwitterBackupDbContext>()
                .AddDefaultTokenProviders();

            //TODO: Decorator pattern
            services.AddMemoryCache(mc => new MemoryCacheOptions());

            services.AddSingleton<IRestClientFactory, RestClientFactory>();
            services.AddSingleton<IRestRequestFactory, RestRequestFactory>();

            //TODO: Extension method
            services.AddSingleton<TwitterAccessTokenProvider, TwitterAccessTokenProvider>(tatp =>
            {
                return new TwitterAccessTokenProvider(consumerKey, consumerSecret);
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
        }
    }
}