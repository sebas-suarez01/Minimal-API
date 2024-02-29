using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Users;

namespace Minimal_API.Persistance.Configurations;

public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => ValueObjectId.Create<UserId>(value)
            );
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(32);
        
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(64);
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(16);
        builder.Property(u => u.EmailConfirmed)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.HasQueryFilter(u => !u.IsDeleted);
        
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();

        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .OnDelete(DeleteBehavior.NoAction);
    }
}