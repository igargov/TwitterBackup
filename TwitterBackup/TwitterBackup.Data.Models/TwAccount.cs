﻿using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Models
{
    public class TwAccount
    {
        public int Id { get; set; }

        public string TwitterId { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public string ProfileImageUrl { get; set; }

        public DateTime? CreatedAtTwitter { get; set; }

        public DateTime? CreatedAt { get; set; }


        public TwAccountImage TwAccountImage { get; set; }

        public List<UserTwAccount> Users { get; set; }
    }
}
