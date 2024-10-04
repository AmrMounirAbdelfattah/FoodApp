using AutoMapper.QueryableExtensions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.Helpers
{
    public static class MapperHelper
    {
        public static IMapper Mapper { get; set; }
        public static TResult MapOne<TResult>(this object source)
        {
            return Mapper.Map<TResult>(source);
        }
        public static IQueryable<TResult> Map<TResult>(this IQueryable<object> source)
        {
            return source.ProjectTo<TResult>(Mapper.ConfigurationProvider);
        }
    }
}
