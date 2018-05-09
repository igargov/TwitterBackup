using System.Collections.Generic;
using System.Linq;

namespace TwitterBackup.Providers
{
    public interface IMappingProvider
    {
        TDestination MapTo<TDestination>(object source);

        TDestination MapTo<TSource, TDestination>(TSource source);

        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source);

        IEnumerable<TDestination> ProjectTo<TDestination>(IEnumerable<object> source);
    }
}
