using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal_API.Domain.Items;

namespace Minimal_API.Persistance.Configurations;

public class ItemModelConfiguration : IEntityTypeConfiguration<ItemModel>
{
    public void Configure(EntityTypeBuilder<ItemModel> builder)
    {
        builder.HasIndex(i => i.Id);
    }
}