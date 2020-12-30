using AutoMapper;

namespace RedmineHelper.Mapper.Abstractions
{
    public abstract class BaseMapper
    {
        public IConfigurationProvider Provider;
        public abstract void Map<TSource, TDestination>(TSource source, TDestination destination);
        public abstract TDestination Map<TSource, TDestination>(object source);
    }
}
