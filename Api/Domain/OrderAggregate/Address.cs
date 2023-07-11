using System;
using System.Collections.Generic;
using Api.Domain.Abstract;

namespace Api.Domain.OrderAggregate;

public sealed class Address : ValueObject
{
  public Address(
    string street,
    string city,
    int zipCode,
    string? state = null
    )
  {
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

  #pragma warning disable 8618
  public Address() { }
  #pragma warning restore 8618

  public string Street { get; private set; }
  public string City { get; private set; }
  public int ZipCode { get; private set; }
  public string? State { get; private set; }
  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Street;
    yield return City;
    yield return ZipCode;
    yield return State;
  }
}