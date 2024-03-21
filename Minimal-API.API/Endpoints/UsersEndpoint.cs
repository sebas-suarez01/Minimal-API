using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal_API.API.Common;
using Minimal_API.API.Requests;
using Minimal_API.Application.Users.Commands.ChangeUserRole;
using Minimal_API.Application.Users.Queries.GetAllUsers;
using Minimal_API.Application.Users.Queries.GetUserByEmail;
using Minimal_API.Application.Users.Queries.GetUserById;
using Minimal_API.Application.Users.Queries.GetUserByUsername;
using Minimal_API.Domain.Enums;
using Minimal_API.Domain.Roles.Shared;
using Minimal_API.Infrastructure.Authentication;

namespace Minimal_API.API.Endpoints;

public static class UsersEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/users", GetAllUsers)
            .WithName(nameof(GetAllUsers))
            .WithDisplayName(nameof(GetAllUsers));
        
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
    
    [HasPermission(RoleMapping.USER, Permission.Read)]
    public static async Task<IResult> GetAllUsers(ISender sender)
    {
        var query = new GetAllUsersQuery();

        var result = await sender.Send(query);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
    [Authorize(Roles = RoleMapping.USER)]
    public static async Task<IResult> GetUserById(Guid id, ISender sender)
    {
        var query = new GetUserByIdQuery(id);

        var result = await sender.Send(query);
        
        if (result.IsSuccess)
            return Results.Ok(result.Value);
        
        var extensions = Utils.ExtensionsReturnValues(
            new KeyValuePair<string, object?>("user_id", id.ToString()));
            
        return Results.Problem(
            statusCode: (int)result.Errors[0].HttpStatusCode,
            detail: result.Errors[0].Description,
            extensions: extensions);
    }

    public static async Task<IResult> GetUserByUsername(string username, ISender sender)
    {
        var query = new GetUserByUsernameQuery(username);

        var result = await sender.Send(query);

        if (result.IsSuccess)
            return Results.Ok(result.Value);
        
        var extensions = Utils.ExtensionsReturnValues(
            new KeyValuePair<string, object?>("username", username));
            
        return Results.Problem(
            statusCode: (int)result.Errors[0].HttpStatusCode,
            detail: result.Errors[0].Description,
            extensions: extensions);

    }
    public static async Task<IResult> GetUserByEmail(string email, ISender sender)
    {
        var query = new GetUserByEmailQuery(email);

        var result = await sender.Send(query);
        
        if (result.IsSuccess)
            return Results.Ok(result.Value);
        
        var extensions = Utils.ExtensionsReturnValues(
            new KeyValuePair<string, object?>("email", email));
            
        return Results.Problem(
            statusCode: (int)result.Errors[0].HttpStatusCode,
            detail: result.Errors[0].Description,
            extensions: extensions);
    }

    public static async Task<IResult> ChangeUserRole(Guid id, [FromBody]ChangeUserRoleRequest request, ISender sender)
    {
        var command = new ChangeUserRoleCommand(id, request.RoleName);

        var result = await sender.Send(command);
        
        if (result.IsSuccess)
            return Results.NoContent();
        
        var extensions = Utils.ExtensionsReturnValues(
            new KeyValuePair<string, object?>("user_id", id.ToString()));
            
        return Results.Problem(
            statusCode: (int)result.Errors[0].HttpStatusCode,
            detail: result.Errors[0].Description,
            extensions: extensions);
    }
}