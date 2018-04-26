using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services
{
    public interface ITwAccountService
    {
        int SaveAccount(TwAccountViewModel model);
    }
}
