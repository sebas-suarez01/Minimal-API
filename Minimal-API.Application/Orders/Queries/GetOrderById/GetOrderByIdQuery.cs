using Minimal_API.Application.Abstractions;
using Minimal_API.Domain.Orders;

namespace Minimal_API.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id): IQuery<OrderDto>;