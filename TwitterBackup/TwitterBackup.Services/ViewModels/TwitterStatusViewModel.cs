namespace TwitterBackup.Services.ViewModels
{
    public class TwitterStatusViewModel
    {
        public int Id { get; set; }

        public string TwitterStatusId { get; set; }

        public string Text { get; set; }

        public int RetweetCount { get; set; }

        public int FavoriteCount { get; set; }
    }
}
