﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Minimal_API.Application.Behaviors;

namespace Minimal_API.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SaveChangesBehavior<,>));
        
        return services;
    }
}