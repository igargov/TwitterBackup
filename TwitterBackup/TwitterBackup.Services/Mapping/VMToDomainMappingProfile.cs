using AutoMapper;
using TwitterBackup.Data.Models;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services.Mapping
{
    public class VMToDomainMappingProfile : Profile
    {
        public VMToDomainMappingProfile()
        {
            CreateMap<TwitterAccountDTO, TwitterAccount>()
                .ForMember(d => d.CreatedAtTwitter, opt => opt.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.TwitterId, opt => opt.MapFrom(s => s.IdString));
        }
    }
}
