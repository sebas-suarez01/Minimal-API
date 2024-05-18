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
        services.AddDbContext<AgencyDbContext>((sp, opt)=>
        {
            var publisherService = sp.GetService<IPublisher>();

            var publishDomainEventInterceptor = sp.GetService<PublishDomainEventInterceptor>();
            
            opt.UseNpgsql(configuration.GetConnectionString("AgencyDbConnectionDocker"))
                .AddInterceptors(new PublishDomainEventInterceptor(publisherService!));
        });
        
        return services;
    }
}