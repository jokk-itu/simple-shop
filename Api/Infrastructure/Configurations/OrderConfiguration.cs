using Api.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
          .HasMany(x => x.OrderItems)
          .WithOne(x => x.Order);

        builder
          .Property(x => x.DeliveryMethod)
          .HasConversion<int>();

        builder
          .Property(x => x.PaymentMethod)
          .HasConversion<int>();

        builder.OwnsOne(x => x.Address);
        builder.Navigation(x => x.Address).IsRequired();

        builder.OwnsOne(x => x.Contact);
        builder.Navigation(x => x.Contact).IsRequired();
    }
}