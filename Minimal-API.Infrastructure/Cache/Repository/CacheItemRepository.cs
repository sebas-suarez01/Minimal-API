using Microsoft.Extensions.Caching.Memory;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Application.Items.Queries.GetAllItems;
using Minimal_API.Domain.DomainEvents;
using Minimal_API.Domain.Items;
using Minimal_API.Domain.Shared;
using Minimal_API.Infrastructure.Repository;

namespace Minimal_API.Infrastructure.Cache.Repository;

public class CacheItemRepository : IItemRepository
{
    private readonly ItemRepository _decorated;
    private readonly IMemoryCacheService _memoryCache;

    public CacheItemRepository(ItemRepository decorated, IMemoryCacheService memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _decorated.DeleteAsync(id, cancellationToken);
    }

    public async Task<Result<Guid>> CreateAsync(ItemModel model, CancellationToken cancellationToken = default)
    {
        var itemIdResult = await _decorated.CreateAsync(model, cancellationToken);

        if (itemIdResult.IsFailure)
            return Result.Failure<Guid>(itemIdResult.Errors);
        
        model.RaiseDomainEvent(new ItemCreatedDomainEvent(new Guid(), model.Id, model.Name, model.Price));

        return itemIdResult.Value;
    }

    public Task<Result<ItemDto>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _decorated.GetByNameAsync(name, cancellationToken);
    }

    public async Task<Result<IEnumerable<ItemDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var key = $"{nameof(GetAllItemsQuery)}-{nameof(ItemModel)}";
        return await _memoryCache.GetOrCreateAsync(
            key, 
            _=>_decorated.GetAllAsync(cancellationToken),
            TimeSpan.FromSeconds(100),
            cancellationToken);
    }
}