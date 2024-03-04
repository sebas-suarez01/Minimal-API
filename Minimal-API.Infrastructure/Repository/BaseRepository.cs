using Microsoft.EntityFrameworkCore;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Errors;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Shared;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure.Repository;

public class BaseRepository<T, TId> : IRepository<T, TId>
    where T : Entity<TId>
    where TId : ValueObjectId
{
    protected readonly AgencyDbContext _context;

    public BaseRepository(AgencyDbContext context)
    {
        _context = context;
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<T>().SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
            return Result.Failure(ErrorTypes.Models.IdNotFound(id));

        entity.IsDeleted = true;

        return Result.Success();
    }

    public async Task<Result<Guid>> CreateAsync(T model, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(model, cancellationToken);

        return model.Id.Value;
    }
}