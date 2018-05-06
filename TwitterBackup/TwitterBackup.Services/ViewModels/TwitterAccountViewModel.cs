namespace TwitterBackup.Services.ViewModels
{
    public class TwitterAccountViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public int StatusesCount { get; set; }

        public string Name { get; set; }

        public string ProfileImageUrl { get; set; }

        public string ProfileImage { get; set; }

        public string ScreenName { get; set; }

        public string Url { get; set; }
    }
}