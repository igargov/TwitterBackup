using TwitterBackup.Data.Models;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services
{
    public class TwitterAccountService : ITwitterAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mappingProvider;

        public TwitterAccountService(IUnitOfWork unitOfWork, IMappingProvider mappingProvider)
        {
            this.unitOfWork = unitOfWork;
            this.mappingProvider = mappingProvider;
        }

        public int SaveAccount(TwitterAccountViewModel model)
        {
            var domainModel = this.mappingProvider.MapTo<TwitterAccount>(model);

            this.unitOfWork.TwitterAccounts.Add(domainModel);

            return this.unitOfWork.SaveChanges();
        }
    }
}