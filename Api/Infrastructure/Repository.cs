using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Domain.Abstract;
using Api.Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;
public class Repository<T> : IRepository<T>
  where T : AggregateRoot
{
  private readonly ShopContext _shopContext;

  public Repository(ShopContext shopContext)
  {
    _shopContext = shopContext;
  }

  public Task<T?> GetById(int id, CancellationToken cancellationToken = default)
  {
    return _shopContext
      .Set<T>()
      .Where(x => x.Id == id)
      .SingleOrDefaultAsync(cancellationToken: cancellationToken);
  }

  public Task<List<T>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
  {
    return _shopContext
      .Set<T>()
      .Where(x => ids.Contains(x.Id))
      .ToListAsync(cancellationToken: cancellationToken);
  }

  public async Task Add(T aggregateRoot, CancellationToken cancellationToken = default)
  {
    await _shopContext
      .Set<T>()
      .AddAsync(aggregateRoot, cancellationToken: cancellationToken);
  }

  public void Remove(T aggregateRoot)
  {
    _shopContext
      .Set<T>()
      .Remove(aggregateRoot);
  }
}