using MediatR;
using Minimal_API.Application.Users.Queries.GetAllUsers;

namespace Minimal_API.API.Endpoints;

public static class UsersEndpoint
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.Map("api/users", async (ISender sender) =>
        {
            var query = new GetAllUsersQuery();

            var result = await sender.Send(query);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                    detail: result.Errors[0].Description);
        });
    }
}