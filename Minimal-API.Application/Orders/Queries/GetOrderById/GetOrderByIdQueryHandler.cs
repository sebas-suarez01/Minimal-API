using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _repository;
    private readonly IDistributedCacheService _distributedCache;

    public GetOrderByIdQueryHandler(IOrderRepository repository, IDistributedCacheService distributedCache)
    {
        _repository = repository;
        _distributedCache = distributedCache;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken=default)
    {
        var key = $"{nameof(OrderDto)}-{request.Id}";
        OrderDto? orderCached = await _distributedCache.GetAsync<OrderDto>(key, cancellationToken);

        if (orderCached is not null)
            return orderCached;
        
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (order.IsFailure)
            return Result.Failure<OrderDto>(order.Errors);

        await _distributedCache.SetAsync(key, order.Value, cancellationToken);

        return order.Value;
    }
}