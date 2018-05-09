namespace TwitterBackup.Data.Models
{
    public class TwitterAccountImage
    {
        public int Id { get; set; }

        public string ProfileImage { get; set; }

        public int TwitterAccountId { get; set; }
        public TwitterAccount TwitterAccount { get; set; }
    }
}
