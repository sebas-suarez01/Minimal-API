using System.Data;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Interfaces;

public interface IUnitOfWork
{
    public Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken);
    public IDbTransaction BeginTransaction();
}