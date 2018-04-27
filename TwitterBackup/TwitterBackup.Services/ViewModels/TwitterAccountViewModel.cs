using Newtonsoft.Json;
using System;

namespace TwitterBackup.Services.ViewModels
{
    public class TwitterAccountViewModel
    {
        public DateTime CreatedAt { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAtTwitter { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }
        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
        [JsonProperty("id_str")]
        public string TwitterId { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}