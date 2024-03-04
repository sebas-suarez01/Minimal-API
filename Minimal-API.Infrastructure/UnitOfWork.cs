using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Shared;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AgencyDbContext _context;

    public UnitOfWork(AgencyDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }
}