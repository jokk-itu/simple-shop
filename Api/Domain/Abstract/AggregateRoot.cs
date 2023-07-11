using System;
using System.Collections.Generic;

namespace Api.Domain.Abstract;

public abstract class AggregateRoot : Entity
{
  private readonly IList<Func<IDomainEvent>> _domainEvents = new List<Func<IDomainEvent>>();
  public IReadOnlyCollection<Func<IDomainEvent>> DomainEvents => _domainEvents.AsReadOnly();

  protected void RaiseEvent(Func<IDomainEvent> domainEventInvocation)
  {
    _domainEvents.Add(domainEventInvocation);
  }
}