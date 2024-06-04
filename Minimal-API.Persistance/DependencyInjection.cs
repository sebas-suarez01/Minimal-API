using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minimal_API.Persistance.Interceptors;

namespace Minimal_API.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
    {
        var dbHost = Environment.GetEnvironmentVariable("HOST");
        var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB");
        var userName = Environment.GetEnvironmentVariable("POSTGRES_USER");
        var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

        var connectionString = string.Empty;
        if (string.IsNullOrEmpty(dbHost) || 
            string.IsNullOrEmpty(dbName) || 
            string.IsNullOrEmpty(userName) || 
            string.IsNullOrEmpty(password))
        {
            connectionString = configuration.GetConnectionString("AgencyDbConnectionDocker");
        }
        else
        {
            connectionString = $"Host={dbHost};Database={dbName};Username={userName};Password={password}";
        }
        
        services.AddDbContext<AgencyDbContext>((sp, opt)=>
        {
            var publisherService = sp.GetService<IPublisher>();

            var publishDomainEventInterceptor = sp.GetService<PublishDomainEventInterceptor>();
            
            opt.UseNpgsql(connectionString)
                .AddInterceptors(new PublishDomainEventInterceptor(publisherService!));
        });
        
        return services;
    }
}