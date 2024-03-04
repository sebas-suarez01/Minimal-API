using Microsoft.Extensions.DependencyInjection;
using Minimal_API.Application.Interfaces;
using Minimal_API.Infrastructure.Repository;

namespace Minimal_API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}