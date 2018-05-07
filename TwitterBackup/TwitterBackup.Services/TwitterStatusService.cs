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
    public class TwitterStatusService : ITwitterStatusService
    {
        private IMappingProvider mappingProvider;
        private IUnitOfWork unitOfWork;

        public TwitterStatusService(IUnitOfWork unitOfWork, IMappingProvider mappingProvider)
        {
            this.unitOfWork = unitOfWork;
            this.mappingProvider = mappingProvider;
        }

        public List<TwitterStatusViewModel> GetAll(int userId)
        {
            var statuses = this.unitOfWork.TwitterStatuses
                .All()
                .Where(ts => ts.Users.Any(u => u.UserId == userId));

            var statusesModel = this.mappingProvider.ProjectTo<TwitterStatusViewModel>(statuses).ToList();

            return statusesModel;
        }

        public int Create(TwitterStatusDTO model, int userId)
        {
            try
            {
                var status = this.unitOfWork.TwitterStatuses
                    .All()
                    .Where(ts => ts.TwitterStatusId.Equals(model.IdString))
                    .FirstOrDefault();

                if (status == null)
                {
                    status = this.mappingProvider.MapTo<TwitterStatus>(model);
                    status.CreatedAt = DateTime.Now;
                    status.Users.Add(new UserTwitterStatus()
                    {
                        UserId = userId,
                        TwitterStatus = status
                    });

                    this.unitOfWork.TwitterStatuses.Add(status);
                }
                else
                {
                    status.Users.Add(new UserTwitterStatus()
                    {
                        UserId = userId,
                        TwitterStatus = status
                    });
                }

                this.unitOfWork.SaveChanges();

                return status.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public bool Delete(int statusId, int userId)
        {
            try
            {
                var result = this.unitOfWork.UserTwitterStatuses
                    .All()
                    .Where(uts => uts.TwitterStatusId == statusId && uts.UserId == userId)
                    .FirstOrDefault();

                this.unitOfWork.UserTwitterStatuses.Delete(result);
                this.unitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
