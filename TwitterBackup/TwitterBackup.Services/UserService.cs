using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;
        private IMappingProvider mappingProvider;

        public UserService(IUnitOfWork unitOfWork, IMappingProvider mappingProvider)
        {
            this.unitOfWork = unitOfWork;
            this.mappingProvider = mappingProvider;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var userids = this.unitOfWork.Context.UserRoles.Where(a => a.RoleId != 1).Select(b => b.UserId).Distinct();
            var domainModels = this.unitOfWork.Context.Users.Where(a => userids.Any(c => c == a.Id));

            var viewModels = this.mappingProvider.ProjectTo<UserViewModel>(domainModels);
            return viewModels;
        }

        public void DeleteUser(User user)
        {
            this.unitOfWork.Users.Delete(user);
        }
    }
}