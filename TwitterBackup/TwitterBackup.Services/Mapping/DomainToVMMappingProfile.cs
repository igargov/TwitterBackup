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
            CreateMap<TwitterAccountDTO, TwitterAccountViewModel>()
                .ForMember(vm => vm.TwitterId, opt => opt.MapFrom(d => d.IdString));
            CreateMap<TwitterErrorDTO, TwitterErrorViewModel>();
        }
    }
}