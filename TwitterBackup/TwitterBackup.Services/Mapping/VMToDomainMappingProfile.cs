using AutoMapper;
using TwitterBackup.Data.Models;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services.Mapping
{
    public class VMToDomainMappingProfile : Profile
    {
        public VMToDomainMappingProfile()
        {
            CreateMap<TwitterAccountViewModel, TwitterAccount>();
        }
    }
}
