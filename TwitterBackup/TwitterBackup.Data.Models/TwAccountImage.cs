namespace TwitterBackup.Data.Models
{
    public class TwAccountImage
    {
        public int Id { get; set; }

        public byte[] ProfileImage { get; set; }

        public int TwAccountId { get; set; }
        public TwAccount TwAccount { get; set; }
    }
}
