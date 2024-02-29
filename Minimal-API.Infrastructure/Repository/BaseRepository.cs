using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Shared;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure.Repository;

public class BaseRepository<T, TId, TDto> : IRepository<T, TId, TDto>
    where T: Entity<TId>
    where TId: ValueObjectId
{
    private readonly AgencyDbContext _context;

    public BaseRepository(AgencyDbContext context)
    {
        _context = context;
    }

    public Task<Result<IEnumerable<TDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<TDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid>> Create(T model)
    {
        throw new NotImplementedException();
    }
}