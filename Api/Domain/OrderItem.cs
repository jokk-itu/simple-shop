using System;
using Api.Domain.Abstract;
using Api.Domain.ItemAggregate;
using Api.Domain.OrderAggregate;

namespace Api.Domain;

public sealed class OrderItem : Entity
{
  public OrderItem(
    Item item,
    Order order,
    int quantity)
  {
    if (quantity < 1)
    {
      throw new ArgumentException("must not be less than 1", nameof(quantity));
    }

    Item = item;
    Order = order;
    Quantity = quantity;
  }

#pragma warning disable CS8618 // empty constructor exists for EF Core
  private OrderItem()
  {
  }
#pragma warning restore CS8618

  public Item Item { get; private set; }
  public Order Order { get; private set; }
  public int Quantity { get; private set; }
}