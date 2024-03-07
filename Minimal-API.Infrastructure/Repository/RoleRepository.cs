using Microsoft.EntityFrameworkCore;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Errors;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Roles;
using Minimal_API.Domain.Shared;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly AgencyDbContext _context;

    public RoleRepository(AgencyDbContext context)
    {
        _context = context;
    }

    public async Task<Result<RoleModel>> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var role = await _context.Set<RoleModel>()
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == ValueObjectId.Create<RoleId>(id), cancellationToken);

        return role ?? Result.Failure<RoleModel>(ErrorTypes.Models.IdNotFound(id));
    }

    public async Task<Result<RoleModel>> GetRoleByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var role = await _context.Set<RoleModel>()
            .Include(r=> r.Users)
            .SingleOrDefaultAsync(r => r.Name == name, cancellationToken);

        return role ?? Result.Failure<RoleModel>(ErrorTypes.Models.RoleNotFound(name));
    }
}