using AutoMapper;
using Financial.Application.Mappings;

namespace Financial.API.Configuration
{
    public static class MappingConfiguration
    {
        public static void AddMappingConfiguration(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new FinancialMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}