using TwitterBackup.Data.Models.Identity;

namespace TwitterBackup.Data.Models
{
    public class UserTwAccount
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TwAccountId { get; set; }
        public TwAccount TwAccount { get; set; }
    }
}
