using Microsoft.Extensions.DependencyInjection;
using TwitterBackup.TwitterApiClient;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.TwitterApiClient.RestClientFactory;

namespace TwitterBackup.Web.Extensions
{
    public static class TwitterApiClientExtensions
    {
        public static IServiceCollection AddTwitterApiClient(this IServiceCollection services, string consumerKey, string consumerSecret)
        {
            services.AddSingleton<IRestClientFactory, RestClientFactory>();
            services.AddSingleton<IRestRequestFactory, RestRequestFactory>();

            services.AddSingleton<TwitterAccessTokenProvider, TwitterAccessTokenProvider>(tatp =>
            {
                return new TwitterAccessTokenProvider(consumerKey, consumerSecret);
            });

            services.AddScoped<ITwitterApiService, TwitterApiService>();

            return services;
        }
    }
}
