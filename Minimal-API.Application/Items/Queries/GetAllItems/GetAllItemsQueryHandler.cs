using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Items;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Items.Queries.GetAllItems;

public class GetAllItemsQueryHandler : IQueryHandler<GetAllItemsQuery, IEnumerable<ItemDto>>
{
    private readonly IItemRepository _repository;

    public GetAllItemsQueryHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ItemDto>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken=default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}