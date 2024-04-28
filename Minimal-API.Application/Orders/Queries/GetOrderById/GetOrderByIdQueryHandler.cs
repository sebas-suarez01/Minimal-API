using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _repository;

    public GetOrderByIdQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken=default)
    {
        return await _repository.GetByIdAsync(request.Id, cancellationToken);
    }
}