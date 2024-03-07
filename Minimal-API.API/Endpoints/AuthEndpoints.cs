using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minimal_API.API.Requests;
using Minimal_API.Application.Auth.Commands.Login;
using Minimal_API.Application.Auth.Commands.Register;

namespace Minimal_API.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/register", Register)
            .WithName(nameof(Register))
            .WithDisplayName(nameof(Register));
        app.MapPost("api/auth/login", Login)
            .WithName(nameof(Login))
            .WithDisplayName(nameof(Login));
    }

    public static async Task<IResult> Register([FromBody]RegisterRequest request, ISender sender)
    {
        var command = new RegisterCommand(request.Username, request.Name, request.LastName, request.Email,
            request.PhoneNumber, request.Password, request.ConfirmPassword);

        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
    public static async Task<IResult> Login([FromBody]LoginRequest request, ISender sender)
    {
        var command = new LoginCommand(request.Username, request.Password);

        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
}