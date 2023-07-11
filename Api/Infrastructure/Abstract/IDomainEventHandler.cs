using System.Threading;
using System.Threading.Tasks;
using Api.Domain.Abstract;

namespace Api.Infrastructure.Abstract;

public interface IDomainEventHandler<in TDomainEvent>
  where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
}