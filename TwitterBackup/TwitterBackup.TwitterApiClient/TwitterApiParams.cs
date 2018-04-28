namespace TwitterBackup.TwitterApiClient
{
    public static class TwitterApiParams
    {
        public static readonly string BaseUrl = "https://api.twitter.com/1.1/";
        public static readonly string TokenEndpoint = "https://api.twitter.com/oauth2/token";
        public static readonly string UsersShowEndpoint = "users/show.json";
        public static readonly string StatusesUserTimelineEndpoint = "statuses/user_timeline.json";
        public static readonly string GrantType = "client_credentials";
    }
}
