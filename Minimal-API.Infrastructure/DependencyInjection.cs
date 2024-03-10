﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minimal_API.Application.Interfaces;
using Minimal_API.Infrastructure.Authentication;
using Minimal_API.Infrastructure.Authentication.Jwt;
using Minimal_API.Infrastructure.Repository;

namespace Minimal_API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtOptions();
        configuration.Bind(JwtOptions.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

        return services;
    }
}