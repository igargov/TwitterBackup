﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterDTOs;

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

        public List<TwitterAccountWithImageViewModel> GetAll()
        {
            var allAccounts = this.unitOfWork.TwitterAccounts
                .All()
                .Include(tai => tai.TwitterAccountImage);

            var allAccountModel = this.mappingProvider.ProjectTo<TwitterAccountWithImageViewModel>(allAccounts).ToList();

            return allAccountModel;
        }

        public int Create(TwitterAccountDTO model, int userId, string picBase64)
        {
            try
            {
                var dbModel = this.unitOfWork.TwitterAccounts
                    .All()
                    .Where(t => t.TwitterId.Equals(model.IdString))
                    .FirstOrDefault();

                if (dbModel != null)
                {
                    return -1;
                }

                var twitterAccount = this.mappingProvider.MapTo<TwitterAccount>(model);

                twitterAccount.CreatedAt = DateTime.Now;
                twitterAccount.Users.Add(new UserTwitterAccount()
                {
                    TwitterAccount = twitterAccount,
                    UserId = userId
                });

                if (!string.IsNullOrEmpty(picBase64))
                {
                    twitterAccount.TwitterAccountImage = new TwitterAccountImage()
                    {
                        ProfileImage = picBase64,
                        TwitterAccount = twitterAccount
                    };
                }

                this.unitOfWork.TwitterAccounts.Add(twitterAccount);
                this.unitOfWork.SaveChanges();

                return twitterAccount.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int Update(TwitterAccountDTO model)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}