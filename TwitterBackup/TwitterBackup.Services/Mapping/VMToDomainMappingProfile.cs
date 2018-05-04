﻿using AutoMapper;
using TwitterBackup.Data.Models;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services.Mapping
{
    public class VMToDomainMappingProfile : Profile
    {
        public VMToDomainMappingProfile()
        {
            CreateMap<TwitterAccountWithImageViewModel, TwitterAccount>();

            //DTO mapping
            CreateMap<TwitterAccountDTO, TwitterAccount>()
                .ForMember(d => d.CreatedAtTwitter, opt => opt.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.TwitterId, opt => opt.MapFrom(s => s.IdString));
        }
    }
}