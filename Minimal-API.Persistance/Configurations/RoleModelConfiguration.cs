using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Roles;

namespace Minimal_API.Persistance.Configurations;

public class RoleModelConfiguration : IEntityTypeConfiguration<RoleModel>
{
    public void Configure(EntityTypeBuilder<RoleModel> builder)
    {
        builder.ToTable("Roles");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => ValueObjectId.Create<RoleId>(value));
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(16);
        builder.Property(r => r.NormalizedName)
            .IsRequired()
            .HasMaxLength(16);
        
        builder.HasIndex(r => r.Name).IsUnique();
        builder.HasIndex(r => r.NormalizedName).IsUnique();
    
        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .OnDelete(DeleteBehavior.NoAction);
    }
}