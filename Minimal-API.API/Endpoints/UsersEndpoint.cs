using MediatR;

namespace Minimal_API.API.Endpoints;

public static class UsersEndpoint
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.Map("api/users", async (ISender sender) =>
        {
            
        });
    }
}