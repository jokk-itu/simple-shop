using System.Threading.Tasks;
using Api.Infrastructure.Abstract;

namespace Api.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
  private readonly ShopContext _shopContext;

  public UnitOfWork(ShopContext shopContext)
  {
    _shopContext = shopContext;
  }

  public Task Commit()
  {
    return _shopContext.SaveChangesAsync();
  }
}