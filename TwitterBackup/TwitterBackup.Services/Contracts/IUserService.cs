using System.Collections.Generic;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetUsers();
    }
}