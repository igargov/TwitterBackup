using System;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.TwitterApiClient.TwitterModels;

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

        public int Create(TwitterAccountDTO model)
        {
            try
            {
                var twitterAccount = this.mappingProvider.MapTo<TwitterAccount>(model);
                twitterAccount.CreatedAt = DateTime.Now;

                this.unitOfWork.TwitterAccounts.Add(twitterAccount);
                this.unitOfWork.SaveChanges();

                return twitterAccount.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}