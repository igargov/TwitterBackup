using System;
using System.Linq;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services
{
    public class TwitterStatusService : ITwitterStatusService
    {
        private IMappingProvider mapper;
        private IUnitOfWork unitOfWork;

        public TwitterStatusService(IUnitOfWork unitOfWork, IMappingProvider mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public int Create(TwitterStatusDTO model)
        {
            try
            {
                var status = this.unitOfWork.TwitterStatuses
                    .All()
                    .Where(ts => ts.TwitterStatusId.Equals(model.IdString))
                    .FirstOrDefault();

                if (status != null)
                {
                    throw new ArgumentException("Already exists!");
                }

                var account = this.unitOfWork.TwitterAccounts
                    .All()
                    .Where(ta => ta.TwitterId.Equals(model.IdString))
                    .FirstOrDefault();

                if (account == null)
                {
                    throw new ArgumentNullException("No such account in database!");
                }

                status = this.mapper.MapTo<TwitterStatus>(model);
                status.CreatedAt = DateTime.Now;
                status.TwitterAccount = account;

                this.unitOfWork.TwitterStatuses.Add(status);
                this.unitOfWork.SaveChanges();

                return status.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}
