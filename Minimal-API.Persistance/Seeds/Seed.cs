using Microsoft.EntityFrameworkCore;
using Minimal_API.Domain.Roles;
using Minimal_API.Domain.Roles.Shared;

namespace Minimal_API.Persistance.Seeds;

public static class Seed
{
    public static void SeedRoles(this ModelBuilder builder)
    {
        var userId = Guid.Parse("344422fd-b7c4-4aef-b18f-641549cb603e");
        var userRole = RoleModel.Create(userId, RoleMapping.USER);
        
        var adminId = Guid.Parse("44904e59-04c1-4b72-b54c-d2796c842f09");
        var adminRole = RoleModel.Create(adminId, RoleMapping.ADMIN);
        
        var superId = Guid.Parse("f1a229c1-4059-4c72-85b9-9eab5aece972");
        var superRole = RoleModel.Create(superId, RoleMapping.SUPERADMIN);

        builder.Entity<RoleModel>().HasData(userRole, adminRole, superRole);
    }
}