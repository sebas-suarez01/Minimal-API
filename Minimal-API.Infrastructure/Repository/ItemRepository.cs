using Microsoft.EntityFrameworkCore;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Errors;
using Minimal_API.Domain.Items;
using Minimal_API.Domain.Shared;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure.Repository;

public class ItemRepository : BaseRepository<ItemModel, Guid>, IItemRepository
{
    public ItemRepository(AgencyDbContext context) : base(context)
    {
    }
    public async Task<Result<ItemDto>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var item = await _context.Set<ItemModel>()
            .SingleOrDefaultAsync(i => i.Name == name, cancellationToken);

        if (item is null)
        {
            return Result.Failure<ItemDto>(ErrorTypes.Models.ItemNameNotFound(name));
        }
            
        var itemResult = new ItemDto(item.Id, item.Name, item.Price);
        return itemResult;
    }

    public async Task<Result<IEnumerable<ItemDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<ItemModel>()
            .Select(i => new ItemDto(i.Id, i.Name, i.Price))
            .ToListAsync(cancellationToken);
    }
}