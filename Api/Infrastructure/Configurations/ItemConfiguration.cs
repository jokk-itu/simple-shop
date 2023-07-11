using Api.Domain;
using Api.Domain.ItemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
          .HasMany(x => x.OrderItems)
          .WithOne(x => x.Item);

        builder
          .Property(x => x.Price)
          .HasPrecision(14, 2);
    }
}