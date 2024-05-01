namespace Minimal_API.API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"{e.Message}");
            await Results.Problem(
                    title: "Some error occurred", 
                    statusCode: StatusCodes.Status500InternalServerError,
                    detail: e.Message)
                .ExecuteAsync(context);
        }
    }
}