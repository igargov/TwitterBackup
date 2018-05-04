using TwitterBackup.Data.Models.Identity;

namespace TwitterBackup.Data.Models
{
    public class UserTwitterAccount
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TwitterAccountId { get; set; }
        public TwitterAccount TwitterAccount { get; set; }
    }
}
