using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Models
{
    public class TwitterStatus
    {
        public TwitterStatus()
        {
            this.Users = new HashSet<UserTwitterStatus>();
        }

        public int Id { get; set; }

        public string TwitterStatusId { get; set; }

        public string TwitterUserId { get; set; }

        public string Text { get; set; }

        public string InReplyToTwitterStatusId { get; set; }

        public string InReplyToTwitterAccountId { get; set; }

        public string InReplyToScreenName { get; set; }

        public bool IsQuotedStatus { get; set; }

        public int RetweetCount { get; set; }

        public int FavoriteCount { get; set; }

        public string Language { get; set; }

        public string QuotedStatusId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? CreatedAtTwitter{ get; set; }


        public int? TwitterAccountId { get; set; }

        public TwitterAccount TwitterAccount { get; set; }

        public ICollection<UserTwitterStatus> Users { get; set; }
    }
}
