using Minimal_API.Domain.Items;

namespace Minimal_API.Domain.LineItems;

public record LineItemDto(Guid Id, ItemDto Item, int Amount);