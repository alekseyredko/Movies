using Microsoft.Extensions.DependencyInjection;
using Movies.Infrastructure.MappingProfiles;

namespace Movies.Infrastructure.Extensions
{
    public static class AutomapperExtensions
    {
        public static void AddAutomapperAndProfile(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(ApplicationMappingProfile));
        }
    }
}
