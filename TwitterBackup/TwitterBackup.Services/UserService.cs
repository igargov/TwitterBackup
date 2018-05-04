using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<UserViewModel> GetUsers()
        {
            var domainModels = this.unitOfWork.Users.All().AsEnumerable();/*.Where(u => u.Roles.Any(r => r.Name.Equals("Admin")))*/;
            var viewModels = this.mappingProvider.ProjectTo<UserViewModel>(domainModels);

            return viewModels;
        }
    }
}