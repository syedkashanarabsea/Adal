using AutoMapper;
using Core.CoreClass;
using Microsoft.AspNetCore.Mvc;

namespace Adal.Utilities
{
    public class UtilitiesClass<TSource, TDestination>
    {
        private MapperConfiguration _config;

        public UtilitiesClass()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
        }

        public void CreateMap()
        {
            _config.CreateMapper().ConfigurationProvider.AssertConfigurationIsValid(); // Ensure mappings are valid
        }

        public TDestination Map(TSource source)
        {
            var mapper = _config.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }
    }
}
