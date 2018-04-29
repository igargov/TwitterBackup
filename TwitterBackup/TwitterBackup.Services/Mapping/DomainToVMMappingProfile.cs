using AutoMapper;
using TwitterBackup.Data.Models;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.TwitterModels;

namespace TwitterBackup.Services.Mapping
{
    public class DomainToVMMappingProfile : Profile
    {
        public DomainToVMMappingProfile()
        {
            CreateMap<TwitterAccount, TwitterAccountViewModel>();
            CreateMap<TwitterAccountDTO, TwitterAccountViewModel>();
        }
    }
}