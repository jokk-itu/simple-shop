using Api.Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Infrastructure.Configurations;

public class AggregateRootConfiguration : IEntityTypeConfiguration<AggregateRoot>
{
  public void Configure(EntityTypeBuilder<AggregateRoot> builder)
  {
    builder
      .Ignore(x => x.DomainEvents);
  }
}