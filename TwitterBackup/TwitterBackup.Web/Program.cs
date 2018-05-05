using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TwitterBackup.Web.Extensions;

namespace TwitterBackup.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).SeedData().Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}