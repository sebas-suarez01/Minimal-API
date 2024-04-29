using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IDistributedCacheService _distributedCache;

    public GetAllOrdersQueryHandler(IOrderRepository repository, IDistributedCacheService distributedCache)
    {
        _repository = repository;
        _distributedCache = distributedCache;
    }

    public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken=default)
    {
        var key = "orders";

        var ordersCached = await _distributedCache.GetAsync<IEnumerable<OrderDto>>(key, cancellationToken);

        if (ordersCached is not null)
        {
            return Result.Create(ordersCached);
        }
        
        var orders = await _repository.GetAllAsync(cancellationToken);
        
        if(orders.IsFailure)
            return Result.Failure<IEnumerable<OrderDto>>(orders.Errors);

        await _distributedCache.SetAsync(key, orders.Value, cancellationToken);

        return Result.Create(orders.Value);
    }
}