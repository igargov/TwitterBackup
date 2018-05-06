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
            CreateMap<TwitterAccount, TwitterAccountWithImageViewModel>()
                .ForMember(d => d.ProfileImage, opt => opt.MapFrom(s => s.TwitterAccountImage.ProfileImage));

            CreateMap<User, UserViewModel>();

            //DTO mapping
            CreateMap<TwitterAccountDTO, TwitterAccountViewModel>();
            CreateMap<TwitterStatusDTO, TwitterStatusViewModel>()
                .ForMember(d => d.TwitterStatusId, opt => opt.MapFrom(s => s.IdString));
            CreateMap<TwitterErrorDTO, TwitterErrorViewModel>();
        }
    }
}