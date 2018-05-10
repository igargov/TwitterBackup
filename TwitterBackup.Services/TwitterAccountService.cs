using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Models;

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

        public TwitterAccountViewModel GetById(int accountId, int userId)
        {
            var account = this.unitOfWork.TwitterAccounts
                .All()
                .Include(tai => tai.TwitterAccountImage)
                .Where(ta => ta.Id == accountId && ta.Users.Any(u => u.UserId == userId))
                .FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            var accountModel = this.mappingProvider.MapTo<TwitterAccountViewModel>(account);

            return accountModel;
        }

        public List<TwitterAccountViewModel> GetAll(int userId)
        {
            var allAccounts = this.unitOfWork.TwitterAccounts
                .All()
                .Where(ta => ta.Users.Any(u => u.UserId == userId))
                .Include(tai => tai.TwitterAccountImage);

            var allAccountModel = this.mappingProvider.ProjectTo<TwitterAccountViewModel>(allAccounts).ToList();

            return allAccountModel;
        }

        public int Create(TwitterAccountDTO model, int userId, string picBase64)
        {
            try
            {
                var account = this.unitOfWork.TwitterAccounts
                    .All()
                    .Where(ta => ta.TwitterId.Equals(model.IdString))
                    .FirstOrDefault();

                if (account == null)
                {
                    account = this.BuildTwitterAccountObject(model, userId, picBase64);

                    this.unitOfWork.TwitterAccounts.Add(account);
                }
                else
                {
                    account.Users.Add(new UserTwitterAccount()
                    {
                        UserId = userId,
                        TwitterAccount = account
                    });
                }

                this.AddToAccountStatuses(account.TwitterId, account);

                this.unitOfWork.SaveChanges();

                return account.Id;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error", ex);
            }
        }

        public int Update(TwitterAccountDTO model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int accountId, int userId)
        {
            try
            {
                var result = this.unitOfWork.UserTwitterAccounts
                    .All()
                    .Where(uta => uta.TwitterAccountId == accountId && uta.UserId == userId)
                    .FirstOrDefault();

                this.unitOfWork.UserTwitterAccounts.Delete(result);
                this.unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int IsAccountPresent(string twitterId, int userId)
        {
            var result = this.unitOfWork.UserTwitterAccounts
                .All()
                .Join(this.unitOfWork.TwitterAccounts.All(),
                    uta => uta.TwitterAccountId,
                    (TwitterAccount ta) => ta.Id,
                    (uta, ta) => new { uta.UserId, ta.TwitterId, ta.Id})
                .Where(a => a.UserId == userId && a.TwitterId.Equals(twitterId))
                .FirstOrDefault();

            if (result == null)
            {
                return 0;
            }

            return result.Id;
        }

        private TwitterAccount BuildTwitterAccountObject(TwitterAccountDTO model, int userId, string picBase64)
        {
            var account = this.mappingProvider.MapTo<TwitterAccount>(model);
            account.CreatedAt = DateTime.Now;
            account.Users.Add(new UserTwitterAccount()
            {
                UserId = userId,
                TwitterAccount = account
            });

            if (!string.IsNullOrEmpty(picBase64))
            {
                account.TwitterAccountImage = new TwitterAccountImage()
                {
                    ProfileImage = picBase64,
                    TwitterAccount = account
                };
            }

            return account;
        }

        private void AddToAccountStatuses(string accountTwitterId, TwitterAccount account)
        {
            var statuses = this.unitOfWork.TwitterStatuses
                .All()
                .Where(ts => ts.TwitterUserId.Equals(accountTwitterId))
                .ToList();

            if (statuses != null)
            {
                foreach (var status in statuses)
                {
                    status.TwitterAccount = account;
                }
            }
        }
    }
}