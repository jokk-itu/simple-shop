using Api.Domain.Abstract;

namespace Api.Domain.ItemAggregate;

public class ItemAddedToOrderEvent : IDomainEvent
{
    public ItemAddedToOrderEvent(
      int itemId)
    {
        ItemId = itemId;
    }

    public int ItemId { get; init; }
}