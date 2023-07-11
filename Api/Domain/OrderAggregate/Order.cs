using System;
using System.Collections.Generic;
using Api.Domain.Abstract;

namespace Api.Domain.OrderAggregate;

public sealed class Order : AggregateRoot
{
  public Order(
    Contact contact,
    Address address,
    PaymentMethod paymentMethod,
    DeliveryMethod deliveryMethod,
    DateTime deliveryAt,
    string? instructions = null)
  {
    Contact = contact;
    Address = address;
    PaymentMethod = paymentMethod;
    DeliveryMethod = deliveryMethod;
    DeliveryAt = deliveryAt;
    Instructions = instructions;
  }

  #pragma warning disable 8618 // Empty constructor used by EF
  public Order() { }
  #pragma warning restore 8618

  public Contact Contact { get; private set; }
  public Address Address { get; private set; }
  public PaymentMethod PaymentMethod { get; private set; }
  public DeliveryMethod DeliveryMethod { get; private set; }
  public DateTime DeliveryAt { get; private set; }
  public string? Instructions { get; private set; }

  private readonly IList<OrderItem> _orderItems = new List<OrderItem>();
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
}