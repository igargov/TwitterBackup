using System;
using System.Collections.Generic;
using TwitterBackup.Data.UnitOfWork;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;
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

        public List<TwitterStatusViewModel> GetAll(int accountId)
        {
            throw new NotImplementedException();
        }

        public int Create(TwitterStatusDTO model)
        {
            try
            {
                throw new NotImplementedException();
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
