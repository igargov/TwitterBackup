using Newtonsoft.Json;
using System;

namespace TwitterBackup.TwitterApiClient.TwitterModels
{
    public class TwitterStatusDTO
    {
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("id_str")]
        public string IdString { get; set; }

        public string Text { get; set; }

        public string InReplyToStatusIdStr { get; set; }

        public string InReplyToUserIdStr { get; set; }

        public string InReplyToScreenName { get; set; }

        public TwitterAccountDTO User { get; set; }

        [JsonProperty("is_quote_status")]
        public bool? IsQuotedStatus { get; set; }

        public string QuotedStatusIdStr { get; set; }

        public TwitterStatusDTO QuotedStatus { get; set; }

        public int RetweetCount { get; set; }

        public int FavouriteCount { get; set; }

        [JsonProperty("favourited")]
        public bool? IsFavourited { get; set; }

        [JsonProperty("retweeted")]
        public bool? IsRetweeted { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }
    }
}
