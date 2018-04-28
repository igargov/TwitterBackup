﻿using Newtonsoft.Json;
using System;

namespace TwitterBackup.TwitterApiClient.TwitterModels
{
    public class TwitterAccountDTO
    {
        [JsonProperty("id_str")]
        public string IdString { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        [JsonProperty("protected")]
        public bool? IsProtected { get; set; }

        public int FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public int ListedCount { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int FavouritesCount { get; set; }

        public string TimeZone { get; set; }

        [JsonProperty("verified")]
        public bool? IsVerified { get; set; }

        public int StatusesCount { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}