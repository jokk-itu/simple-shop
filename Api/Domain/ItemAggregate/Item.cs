using System;
using System.Collections.Generic;
using Api.Domain.Abstract;
using Api.Domain.OrderAggregate;

namespace Api.Domain.ItemAggregate;

public sealed class Item : AggregateRoot
{
  public Item(string name, decimal price)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ArgumentException("must not be null or only whitespace", nameof(name));
    }

    if (price is < 0 or 0)
    {
      throw new ArgumentException("must not be less than or equal to zero", nameof(price));
    }

    Name = name;
    Price = price;
  }

#pragma warning disable 8618 // Empty constructor used by EF
  private Item()
  {
  }
#pragma warning restore 8618

  public string Name { get; private set; }
  public decimal Price { get; private set; }

  private readonly IList<OrderItem> _orderItems = new List<OrderItem>();
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

  public void AddOrder(Order order, int quantity)
  {
    var orderItem = new OrderItem(this, order, quantity);
    _orderItems.Add(orderItem);
    RaiseEvent(() => new ItemAddedToOrderEvent(Id));
  }
}