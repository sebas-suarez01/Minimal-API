using Minimal_API.API.Middlewares;

namespace Minimal_API.API.Extensions;

public static class GlobalExceptionHandlerExtensions
{
   public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
   {
      app.UseMiddleware<GlobalExceptionMiddleware>();
   }
}