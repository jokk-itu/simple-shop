using System;
using System.Collections.Generic;
using Api.Domain.OrderAggregate;

namespace Api.Contracts;

public class PostOrderRequest
{
    public string Name { get; init; } = null!;
    public string? Organization { get; init; }
    public string Street { get; init; } = null!;
    public string City { get; init; } = null!;
    public string? State { get; init; }
    public int ZipCode { get; init; }
    public int PhoneNumber { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public DeliveryMethod DeliveryMethod { get; init; }
    public string Email { get; init; } = null!;
    public DateTime DeliveryAt { get; init; }
    public string? Instructions { get; init; }
    public IEnumerable<OrderItemDto> OrderItems { get; init; } = new List<OrderItemDto>();
}