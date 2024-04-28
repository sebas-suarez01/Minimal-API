using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.LineItems;

namespace Minimal_API.Persistance.Configurations;

public class LineItemModelConfiguration : IEntityTypeConfiguration<LineItemModel>
{
    public void Configure(EntityTypeBuilder<LineItemModel> builder)
    {
        builder.HasIndex(li => li.Id);

        builder.HasOne(li => li.Order)
            .WithMany(o => o.OrderItems);
        builder.HasOne(li => li.Item)
            .WithMany(o => o.ItemOrders);

        builder.HasIndex("ItemId", "OrderId")
            .IsUnique();
    }
}