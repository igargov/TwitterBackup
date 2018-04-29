using Newtonsoft.Json;
using System;

namespace TwitterBackup.Services.ViewModels
{
    public class TwitterAccountViewModel
    {
        public string TwitterId { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public DateTime? CreatedAtTwitter { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}