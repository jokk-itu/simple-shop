using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Domain.Abstract;
using Api.Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Interceptors;

public class DomainEventInterceptor : ISaveChangesInterceptor
{
  private readonly IServiceProvider _serviceProvider;

  public DomainEventInterceptor(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
    CancellationToken cancellationToken = new CancellationToken())
  {
    var aggregateRoots = eventData.Context?.ChangeTracker
                           .Entries<AggregateRoot>()
                           .Select(x => x.Entity)
                           .ToList()
                         ?? new List<AggregateRoot>();

    foreach (var domainEventInvoker in aggregateRoots.SelectMany(x => x.DomainEvents))
    {
      var domainEvent = domainEventInvoker();
      await InvokeDomainEvent(domainEvent, cancellationToken);
    }

    return result;
  }

  private async Task InvokeDomainEvent<T>(T domainEvent, CancellationToken cancellationToken)
    where T : IDomainEvent
  {
    var domainEventType = domainEvent.GetType();
    var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEventType);
    var handlerMethod = handlerType.GetMethod(nameof(IDomainEventHandler<T>.Handle),
      new[] { domainEventType, typeof(CancellationToken) });
    var handlers = _serviceProvider.GetServices(handlerType);

    foreach (var handler in handlers)
    {
      await (Task)handlerMethod!.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
    }
  }

  public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
  {
    throw new NotImplementedException();
  }
}