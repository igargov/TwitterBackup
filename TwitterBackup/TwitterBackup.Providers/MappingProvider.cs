﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;

namespace TwitterBackup.Providers
{ 
    public class MappingProvider : IMappingProvider
    {
        private IMapper mapper;

        public MappingProvider(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }

        public TDestination MapTo<TSource, TDestination>(TSource source)
        {
            return this.mapper.Map<TSource, TDestination>(source);
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source)
        {
            return source.ProjectTo<TDestination>();
        }

        public IEnumerable<TDestination> ProjectTo<TDestination>(IEnumerable<object> source)
        {
            return source.AsQueryable().ProjectTo<TDestination>();
        }
    }
}