using MediatR;
using Minimal_API.API.Requests;
using Minimal_API.Application.Items.Commands.AddItem;
using Minimal_API.Application.Items.Queries.GetAllItems;

namespace Minimal_API.API.Endpoints;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/items", GetAllItems)
            .WithName(nameof(GetAllItems))
            .WithDisplayName(nameof(GetAllItems));
        app.MapPost("api/items", AddItem)
            .WithName(nameof(AddItem))
            .WithDisplayName(nameof(AddItem));
    }

    public static async Task<IResult> GetAllItems(ISender sender)
    {
        var query = new GetAllItemsQuery();

        var result = await sender.Send(query);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }

    public static async Task<IResult> AddItem(AddItemRequest request, ISender sender)
    {
        var command = new AddItemCommand(request.Name, request.Price);

        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
}