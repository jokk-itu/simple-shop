using System;

namespace Api.Domain.OrderAggregate;

public sealed record Address
{
  public Address(
    string street,
    string city,
    int zipCode,
    string? state = null
    )
  {
    Console.WriteLine("Address Constructor");
    if (string.IsNullOrWhiteSpace(street))
    {
      throw new ArgumentException("must not be null or whitespace", nameof(street));
    }

    if (string.IsNullOrWhiteSpace(city))
    {
      throw new ArgumentException("must not be null or whitespace", nameof(city));
    }

    if (zipCode < 0)
    {
      throw new ArgumentException("must not be less than 0", nameof(zipCode));
    }
    Street = street;
    City = city;
    ZipCode = zipCode;
    State = state;
  }

  public string Street { get; private init; }
  public string City { get; private init; }
  public int ZipCode { get; private init; }
  public string? State { get; private init; }
}