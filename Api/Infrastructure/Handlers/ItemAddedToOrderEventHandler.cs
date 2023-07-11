using System.Threading;
using System.Threading.Tasks;
using Api.Domain.ItemAggregate;
using Api.Infrastructure.Abstract;
using Microsoft.Extensions.Logging;

namespace Api.Infrastructure.Handlers;

public class ItemAddedToOrderEventHandler : IDomainEventHandler<ItemAddedToOrderEvent>
{
  private readonly ILogger<ItemAddedToOrderEventHandler> _logger;

  public ItemAddedToOrderEventHandler(
    ILogger<ItemAddedToOrderEventHandler> logger)
  {
    _logger = logger;
  }

  public Task Handle(ItemAddedToOrderEvent domainEvent, CancellationToken cancellationToken)
  {
    _logger.LogInformation("Item with id {ItemId} added to order",
      domainEvent.ItemId);

    return Task.CompletedTask;
  }
}