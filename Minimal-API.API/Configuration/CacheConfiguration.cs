namespace Minimal_API.API.Configuration;

public static class CacheConfiguration
{
    public static IServiceCollection AddCacheConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddMemoryCache();
        services.AddDistributedMemoryCache();
        services.AddStackExchangeRedisCache(redisOpt =>
        {
            var connectionString = configuration.GetConnectionString("RedisDocker");
            redisOpt.Configuration = connectionString;
        });

        return services;
    }
}