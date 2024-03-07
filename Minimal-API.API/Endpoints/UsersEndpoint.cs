using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minimal_API.API.Requests;
using Minimal_API.Application.Users.Commands.ChangeUserRole;
using Minimal_API.Application.Users.Queries.GetAllUsers;
using Minimal_API.Application.Users.Queries.GetUserByEmail;
using Minimal_API.Application.Users.Queries.GetUserById;
using Minimal_API.Application.Users.Queries.GetUserByUsername;
using Minimal_API.Domain.Roles.Shared;

namespace Minimal_API.API.Endpoints;

public static class UsersEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/users", GetAllUsers)
            .WithName(nameof(GetAllUsers))
            .WithDisplayName(nameof(GetAllUsers))
            .RequireAuthorization();
        
        app.MapGet("api/users/{id:Guid}", GetUserById)
            .WithName(nameof(GetUserById))
            .WithDisplayName(nameof(GetUserById));
        
        app.MapGet("api/users/username/{username}", GetUserByUsername)
            .WithName(nameof(GetUserByUsername))
            .WithDisplayName(nameof(GetUserByUsername));
        
        app.MapGet("api/users/email/{email}", GetUserByEmail)
            .WithName(nameof(GetUserByEmail))
            .WithDisplayName(nameof(GetUserByEmail));
        
        app.MapPatch("api/users/{id:Guid}", ChangeUserRole)
            .WithName(nameof(ChangeUserRole))
            .WithDisplayName(nameof(ChangeUserRole));
    }

    public static async Task<IResult> GetAllUsers(ISender sender)
    {
        var query = new GetAllUsersQuery();

        var result = await sender.Send(query);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }

    public static async Task<IResult> GetUserById(Guid id, ISender sender)
    {
        var query = new GetUserByIdQuery(id);

        var result = await sender.Send(query);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }

    public static async Task<IResult> GetUserByUsername(string username, ISender sender)
    {
        var query = new GetUserByUsernameQuery(username);

        var result = await sender.Send(query);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
    public static async Task<IResult> GetUserByEmail(string email, ISender sender)
    {
        var query = new GetUserByEmailQuery(email);

        var result = await sender.Send(query);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }

    public static async Task<IResult> ChangeUserRole(Guid id, [FromBody]ChangeUserRoleRequest request, ISender sender)
    {
        var command = new ChangeUserRoleCommand(id, request.RoleName);

        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Results.NoContent()
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
}