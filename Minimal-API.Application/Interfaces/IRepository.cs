using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Interfaces;

public interface IRepository<T, TId, TDto>
    where T: Entity<TId>
    where TId: ValueObjectId
{
    public Task<Result<IEnumerable<TDto>>> GetAllAsync();
    public Task<Result<TDto>> GetByIdAsync(Guid id);
    public Task<Result> DeleteAsync(Guid id);
    public Task<Result<Guid>> Create(T model);
}