using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Minimal_API.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AgencyDbContext>(opt
            => opt.UseNpgsql(configuration.GetConnectionString("AgencyDbConnection")));

        return services;
    }
}