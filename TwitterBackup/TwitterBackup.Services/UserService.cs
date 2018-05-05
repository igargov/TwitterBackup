using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
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

        public void DeleteUser(User user)
        {
            this.unitOfWork.Users.Delete(user);
        }

    }
}