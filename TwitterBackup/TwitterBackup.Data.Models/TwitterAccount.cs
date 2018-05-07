using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Models
{
    public class TwitterAccount
    {
        public TwitterAccount()
        {
            this.Users = new HashSet<UserTwitterAccount>();
            this.TwitterStatuses = new HashSet<TwitterStatus>();
        }

        public int Id { get; set; }

        public string TwitterId { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int FollowersCount { get; set; }

        public int FriendsCount { get; set; }

        public int ListedCount { get; set; }

        public int FavouritesCount { get; set; }

        public int StatusesCount { get; set; }

        public string Language { get; set; }

        public DateTime? CreatedAtTwitter { get; set; }

        public DateTime? CreatedAt { get; set; }


        public TwitterAccountImage TwitterAccountImage { get; set; }

        public ICollection<UserTwitterAccount> Users { get; set; }

        public ICollection<TwitterStatus> TwitterStatuses { get; set; }
    }
}
