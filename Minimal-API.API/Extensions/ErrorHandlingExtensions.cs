using Microsoft.AspNetCore.Diagnostics;

namespace Minimal_API.API.Extensions;

public static class ErrorHandlingExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();

            var exception = exceptionDetails?.Error;
            
            logger.LogError($"{exception.Message}");
            await Results.Problem(
                    title: "Some error occurred", 
                    statusCode: StatusCodes.Status500InternalServerError,
                    detail: exception.Message)
                .ExecuteAsync(context);
        } );
    }
}