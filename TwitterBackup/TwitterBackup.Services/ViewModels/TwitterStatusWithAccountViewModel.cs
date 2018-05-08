using System.Collections.Generic;

namespace TwitterBackup.Services.ViewModels
{
    public class TwitterStatusWithAccountViewModel
    {
        public List<TwitterStatusViewModel> TwitterStatuses { get; set; }

        public string TwitterAccountName { get; set; }

        public string ProfileImage { get; set; }
    }
}
