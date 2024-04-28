using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minimal_API.API.Common;
using Minimal_API.API.Requests;
using Minimal_API.Application.Orders.Commands.AddOrder;
using Minimal_API.Application.Orders.Queries.GetAllOrders;
using Minimal_API.Application.Orders.Queries.GetOrderById;
using Minimal_API.Application.Requests;

namespace Minimal_API.API.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders", GetAllOrders)
            .WithName(nameof(GetAllOrders))
            .WithDisplayName(nameof(GetAllOrders));
        app.MapGet("api/orders/{id:Guid}", GetOrdersById)
            .WithName(nameof(GetOrdersById))
            .WithDisplayName(nameof(GetOrdersById));
        app.MapPost("api/orders", AddOrder)
            .WithName(nameof(AddOrder))
            .WithDisplayName(nameof(AddOrder));
    }

    public static async Task<IResult> GetAllOrders(ISender sender)
    {
        var query = new GetAllOrdersQuery();

        var result = await sender.Send(query);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
    
    public static async Task<IResult> GetOrdersById(Guid id, ISender sender)
    {
        var query = new GetOrderByIdQuery(id);

        var result = await sender.Send(query);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }
        
        var extensions = Utils.ExtensionsReturnValues(
            new KeyValuePair<string, object?>("order_id", id.ToString()));
            
        return Results.Problem(
            statusCode: (int)result.Errors[0].HttpStatusCode,
            detail: result.Errors[0].Description,
            extensions: extensions);
    }

    public static async Task<IResult> AddOrder([FromBody]AddOrderRequest request, ISender sender)
    {
        var command = new AddOrderCommand(request.UserId, request.Items
                                                                .Select(i=> 
                                                                    new ItemRequestService(i.Name, i.Amount))
                                                                .ToList());

        var result = await sender.Send(command);
        
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(statusCode: (int)result.Errors[0].HttpStatusCode,
                detail: result.Errors[0].Description);
    }
}