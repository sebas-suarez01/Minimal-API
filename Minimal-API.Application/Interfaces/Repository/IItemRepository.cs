using Minimal_API.Domain.Items;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Interfaces.Repository;

public interface IItemRepository : IRepository<ItemModel, Guid>
{
    public Task<Result<ItemDto>> GetByNameAsync(string name, CancellationToken cancellationToken);
    public Task<Result<IEnumerable<ItemDto>>> GetAllAsync(CancellationToken cancellationToken);
}