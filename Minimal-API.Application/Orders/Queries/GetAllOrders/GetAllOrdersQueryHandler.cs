using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken=default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}