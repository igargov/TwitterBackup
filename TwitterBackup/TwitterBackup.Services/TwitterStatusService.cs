using System;
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
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
