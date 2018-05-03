using AutoMapper;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services.Mapping
{
    public class DomainToVMMappingProfile : Profile
    {
        public DomainToVMMappingProfile()
        {
            CreateMap<TwitterAccount, TwitterAccountViewModel>();
            CreateMap<TwitterAccountDTO, TwitterAccountViewModel>();
            CreateMap<TwitterErrorDTO, TwitterErrorViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}