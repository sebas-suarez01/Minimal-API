using MediatR;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Items.Queries.GetAllItems;
using Minimal_API.Domain.DomainEvents;
using Minimal_API.Domain.Items;

namespace Minimal_API.Application.Items;

public class CacheInvalidationItemHandler : INotificationHandler<ItemCreatedDomainEvent>
{
    private readonly IMemoryCacheService _memoryCache;

    public CacheInvalidationItemHandler(IMemoryCacheService memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task Handle(ItemCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal();
    }

    private async Task HandleInternal()
    {
        var key = $"{nameof(GetAllItemsQuery)}-{nameof(ItemModel)}";

        await _memoryCache.RemoveAsync(key);
    }
}