using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterBackup.Data;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers.Contracts;
using AutoMapper;
using TwitterBackup.Providers.TwitterProviders;
using TwitterBackup.Providers;
using TwitterBackup.Services;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers.RestClientProvider;

namespace TwitterBackup.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<TwitterBackupDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AzureSQL")));
            }
            else
            {
                services.AddDbContext<TwitterBackupDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.BuildServiceProvider().GetService<TwitterBackupDbContext>().Database.Migrate();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TwitterBackupDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            //services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IRestClientFactory, RestClientFactory>();
            services.AddScoped<IRestRequestFactory, RestRequestFactory>();
            services.AddScoped<ITwitterFacadeProvider, TwitterFacadeProvider>();
            services.AddScoped<ITwitterAccountService, TwitterAccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMappingProvider, MappingProvider>();

            services.AddScoped<TwitterOAuthProvider, TwitterOAuthProvider>(twitterOAuthProvider =>
            {
                return new TwitterOAuthProvider("vBCinenfbxWDWJRbQlxyZS2Yy", "WYBIL5LV3L0vEzGNC1fx6VsLpB7TWZfSx70nJlUYjpc2843B7p");
            });
            services.AddAutoMapper();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}