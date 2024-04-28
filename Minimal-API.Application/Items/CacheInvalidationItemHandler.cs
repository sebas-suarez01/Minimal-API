using MediatR;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Items.Queries.GetAllItems;
using Minimal_API.Domain.DomainEvents;
using Minimal_API.Domain.Items;

namespace Minimal_API.Application.Items;

public class CacheInvalidationItemHandler : INotificationHandler<ItemCreatedDomainEvent>
{
    private readonly ICacheService _cache;

    public CacheInvalidationItemHandler(ICacheService cache)
    {
        _cache = cache;
    }

    public Task Handle(ItemCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal();
    }

    private async Task HandleInternal()
    {
        var key = $"{nameof(GetAllItemsQuery)}-{nameof(ItemModel)}";

        await _cache.RemoveAsync(key);
    }
}