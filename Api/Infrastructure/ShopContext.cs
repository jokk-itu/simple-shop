using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;

public class ShopContext : DbContext
{
  public ShopContext(DbContextOptions<ShopContext> options) : base(options)
  { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);
  }
}