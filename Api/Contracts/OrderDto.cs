using System;
using Api.Domain.OrderAggregate;

namespace Api.Contracts;

public class OrderDto
{
  public int Id { get; init; }
  public ContactDto Contact { get; init; }
  public AddressDto Address { get; init; }
  public PaymentMethod PaymentMethod { get; init; }
  public DeliveryMethod DeliveryMethod { get; init; }
  public DateTime DeliveryAt { get; init; }
  public string? Instructions { get; init; }
}