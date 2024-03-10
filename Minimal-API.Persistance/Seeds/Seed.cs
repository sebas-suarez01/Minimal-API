using Microsoft.EntityFrameworkCore;
using Minimal_API.Domain.Enums;
using Minimal_API.Domain.Permission;
using Minimal_API.Domain.Roles;
using Minimal_API.Domain.Roles.Shared;

namespace Minimal_API.Persistance.Seeds;

public static class Seed
{
    public static void SeedPermissions(this ModelBuilder builder)
    {
        var permissionIds = new Guid[]
        {
            Guid.Parse("ebb87d24-0197-4842-9eab-f659740fe64a"), 
            Guid.Parse("a2386fdf-4c36-4cb5-a85b-c26d0c491fc4"), 
            Guid.Parse("f59471c8-03a2-4c79-80b0-0505d9acfd43"), 
        };
        var permissions = Enum.GetValues<Permission>()
            .Zip(permissionIds)
            .Select(z => PermissionModel.Create(z.Second, z.First.ToString()))
            .ToList();
        
        builder.Entity<PermissionModel>().HasData(permissions);
    }

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