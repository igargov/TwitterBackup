using System.Collections.Generic;

namespace TwitterBackup.Services.ViewModels
{
    public class TwitterAccountStatusDetailViewModel
    {
        public TwitterAccountViewModel TwitterAccount { get; set; }

        public List<TwitterStatusViewModel> TwitterStatuses { get; set; }
    }
}
