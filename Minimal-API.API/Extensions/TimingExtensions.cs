using Minimal_API.API.Middlewares;

namespace Minimal_API.API.Extensions;

public static class TimingExtensions
{
    public static void UseTiming(this IApplicationBuilder app)
    {
        app.UseMiddleware<TimingMiddleware>();
    }
}