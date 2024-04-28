using Minimal_API.Application.Abstractions;
using Minimal_API.Domain.Items;

namespace Minimal_API.Application.Items.Queries.GetAllItems;

public record GetAllItemsQuery() : IQuery<IEnumerable<ItemDto>>;