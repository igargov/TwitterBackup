using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetUsers();

        void DeleteUser(User user);
    }
}