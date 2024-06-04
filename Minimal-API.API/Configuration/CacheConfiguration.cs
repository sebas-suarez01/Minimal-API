namespace Minimal_API.API.Configuration;

public static class CacheConfiguration
{
    public static IServiceCollection AddCacheConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST");
        var redisPort = Environment.GetEnvironmentVariable("REDIS_PORT");

        var connectionString = string.Empty;
        if (string.IsNullOrEmpty(redisHost) || 
            string.IsNullOrEmpty(redisPort))
        {
            Console.WriteLine("Empty");
            connectionString = configuration.GetConnectionString("RedisDocker");
        }
        else
        {
            Console.WriteLine(redisHost);
            Console.WriteLine(redisPort);
            connectionString = $"{redisHost}:{redisPort}";
        }
        
        services.AddMemoryCache();
        services.AddDistributedMemoryCache();
        services.AddStackExchangeRedisCache(redisOpt =>
        {
            // var connectionString = configuration.GetConnectionString("RedisDocker");
            // redisOpt.Configuration = connectionString;
            redisOpt.Configuration = connectionString;
        });

        return services;
    }
}