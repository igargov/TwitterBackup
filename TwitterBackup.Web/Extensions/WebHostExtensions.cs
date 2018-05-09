using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TwitterBackup.Data;
using TwitterBackup.Web.Seed;

namespace TwitterBackup.Web.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TwitterBackupDbContext>();

                SeedDatabase.CreateRoles(services, context).Wait();
            }

            return host;
        }
    }
}
