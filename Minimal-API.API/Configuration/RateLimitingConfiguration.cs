using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Minimal_API.API.Common;

namespace Minimal_API.API.Configuration;

public static class RateLimitingConfiguration
{
    public static IServiceCollection AddFixedWindowRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("FixedWindowsForUsers", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(5);
                opt.PermitLimit = 10;
                opt.QueueLimit = 10;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });

        return services;
    }

    public static IServiceCollection AddConcurrencyRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = async (context, token) =>
            {
                var extensions = Utils.ExtensionsReturnValues(
                    new KeyValuePair<string, object?>("title", "Too Many Request"),
                    new KeyValuePair<string, object?>("status_code", StatusCodes.Status429TooManyRequests),
                    new KeyValuePair<string, object?>("message", "Too many request, try later"));
                await context.HttpContext.Response.WriteAsJsonAsync(extensions);
            };
            options.AddConcurrencyLimiter("ConcurrencyForItems", opt =>
            {
                opt.PermitLimit = 1;
                opt.QueueLimit = 1;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });

        return services;
    }
    public static IServiceCollection AddConcurrencyGlobalRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = async (context, token) =>
            {
                var extensions = Utils.ExtensionsReturnValues(
                    new KeyValuePair<string, object?>("title", "Too Many Request"),
                    new KeyValuePair<string, object?>("status_code", StatusCodes.Status429TooManyRequests),
                    new KeyValuePair<string, object?>("message", "Too many request, try later"));
                await context.HttpContext.Response.WriteAsJsonAsync(extensions);
            };
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                context => RateLimitPartition.GetConcurrencyLimiter("api-key",
                    factory: _ => new ConcurrencyLimiterOptions()
                    {
                        PermitLimit = 2,
                        QueueLimit = 1,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                    }));
        });

        return services;
    }
}