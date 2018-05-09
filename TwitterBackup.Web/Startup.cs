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
using TwitterBackup.Services.Contracts;
using TwitterBackup.Web.Extensions;

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

                consumerKey = Configuration.GetValue<string>("ConsumerKey");
                consumerSecret = Configuration.GetValue<string>("ConsumerSecret");
            }
            else
            {
                services.AddDbContext<TwitterBackupDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocalSQL")));

                consumerKey = Environment.GetEnvironmentVariable("CONSUMER_KEY", EnvironmentVariableTarget.Machine);
                consumerSecret = Environment.GetEnvironmentVariable("CONSUMER_SECRET", EnvironmentVariableTarget.Machine);
            }

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TwitterBackupDbContext>()
                .AddDefaultTokenProviders();

            services.AddTwitterApiClient(consumerKey, consumerSecret);

            services.AddMemoryCache();

            services.AddScoped<ITwitterAccountService, TwitterAccountService>();
            services.AddScoped<ITwitterStatusService, TwitterStatusService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddAutoMapper();

            services.AddMvc();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<TwitterBackupDbContext>().Database.Migrate();

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