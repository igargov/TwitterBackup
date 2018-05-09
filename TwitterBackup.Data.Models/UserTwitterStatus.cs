using TwitterBackup.Data.Models.Identity;

namespace TwitterBackup.Data.Models
{
    public class UserTwitterStatus
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TwitterStatusId { get; set; }
        public TwitterStatus TwitterStatus { get; set; }
    }
}
