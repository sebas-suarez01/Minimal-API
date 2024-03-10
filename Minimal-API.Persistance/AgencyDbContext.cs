using Microsoft.EntityFrameworkCore;
using Minimal_API.Domain.Permission;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Users;
using Minimal_API.Persistance.Configurations;
using Minimal_API.Persistance.Seeds;

namespace Minimal_API.Persistance;

public class AgencyDbContext : DbContext
{
    public AgencyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserModelConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleModelConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PermissionModelConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserPermissionConfiguration).Assembly);

        modelBuilder.SeedRoles();
        modelBuilder.SeedPermissions();
        
    }
}