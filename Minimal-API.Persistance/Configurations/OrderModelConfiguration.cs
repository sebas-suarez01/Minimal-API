using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.Orders;

namespace Minimal_API.Persistance.Configurations;

public class OrderModelConfiguration : IEntityTypeConfiguration<OrderModel>
{
    public void Configure(EntityTypeBuilder<OrderModel> builder)
    {
        builder.HasIndex(o => o.Id);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders);
    }
}