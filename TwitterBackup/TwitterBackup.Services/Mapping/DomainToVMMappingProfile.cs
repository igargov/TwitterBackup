using AutoMapper;
using TwitterBackup.Data.Models;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services.Mapping
{
    public class DomainToVMMappingProfile : Profile
    {
        public DomainToVMMappingProfile()
        {
            CreateMap<TwitterAccount, TwitterAccountViewModel>();
        }
    }
}