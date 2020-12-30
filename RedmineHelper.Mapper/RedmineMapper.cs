namespace RedmineHelper.Mapper
{
    using System.Reflection;
    using AutoMapper;

    public class RedmineMapper : Abstractions.BaseMapper
    {
        private readonly IMapper Mapper;

        public RedmineMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            Mapper = configuration.CreateMapper();
            Provider = Mapper.ConfigurationProvider;
        }

        public override void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            Mapper.Map(source, destination);
        }

        public override TDestination Map<TSource, TDestination>(object source)
        {
            return Mapper.Map<TDestination>((TSource)source);
        }
    }
}
