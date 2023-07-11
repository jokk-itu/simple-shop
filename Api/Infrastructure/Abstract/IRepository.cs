using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Domain.Abstract;

namespace Api.Infrastructure.Abstract;

public interface IRepository<T>
where T : AggregateRoot
{
    Task<T?> GetById(int id, CancellationToken cancellationToken = default);
    Task<List<T>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    Task Add(T aggregateRoot, CancellationToken cancellationToken = default);
    void Remove(T aggregateRoot);
}