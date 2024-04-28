using Minimal_API.Application.Abstractions;
using Minimal_API.Domain.Orders;

namespace Minimal_API.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery() : IQuery<IEnumerable<OrderDto>>;