using AutoMapper;
using MembersTeams.Application.Mappings;

namespace MembersTeams.API.Configuration
{
    public static class MappingConfiguration
    {
        public static void AddMappingConfiguration(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MembersTeamsMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}