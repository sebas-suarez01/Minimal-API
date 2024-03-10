using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.Permission;

namespace Minimal_API.Persistance.Configurations;

internal sealed class PermissionModelConfiguration : IEntityTypeConfiguration<PermissionModel>
{
    public void Configure(EntityTypeBuilder<PermissionModel> builder)
    {
        builder.ToTable("Permissions");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever();

    }
}