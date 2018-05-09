using AutoMapper;
using TwitterBackup.Data.Models;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Models;

namespace TwitterBackup.Services.Mapping
{
    public class VMToDomainMappingProfile : Profile
    {
        public VMToDomainMappingProfile()
        {
            CreateMap<TwitterAccountViewModel, TwitterAccount>();

            //DTO mapping
            CreateMap<TwitterAccountDTO, TwitterAccount>()
                .ForMember(d => d.CreatedAtTwitter, opt => opt.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.TwitterId, opt => opt.MapFrom(s => s.IdString));

            CreateMap<TwitterStatusDTO, TwitterStatus>()
                .ForMember(d => d.TwitterStatusId, opt => opt.MapFrom(s => s.IdString))
                .ForMember(d => d.InReplyToTwitterStatusId, opt => opt.MapFrom(s => s.InReplyToStatusIdStr))
                .ForMember(d => d.InReplyToTwitterAccountId, opt => opt.MapFrom(s => s.InReplyToUserIdStr))
                .ForMember(d => d.CreatedAtTwitter, opt => opt.MapFrom(s => s.CreatedAt));
        }
    }
}