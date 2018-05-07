using System.Collections.Generic;

namespace TwitterBackup.Services.ViewModels
{
    public class TwitterAccountStatusDetailsViewModel
    {
        public TwitterAccountViewModel TwitterAccount { get; set; }

        public List<TwitterStatusViewModel> TwitterStatuses { get; set; }
    }
}
