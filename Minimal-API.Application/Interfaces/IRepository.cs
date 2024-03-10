using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Interfaces;

public interface IRepository<T, TId>
    where T: Entity<TId>
{
    public Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result<TId>> CreateAsync(T model, CancellationToken cancellationToken = default);
}