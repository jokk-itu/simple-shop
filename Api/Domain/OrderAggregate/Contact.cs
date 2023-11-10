using System;

namespace Api.Domain.OrderAggregate;

public sealed record Contact
{
  public Contact(
    string name,
    int phoneNumber,
    string email,
    string? organization = null)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ArgumentException("must not be null or whitespace", nameof(name));
    }

    if(string.IsNullOrWhiteSpace(email))
    {
      throw new ArgumentException("must not be null or whitespace", nameof(email));
    }

    if (phoneNumber < 0)
    {
      throw new ArgumentException("must not be less than 0", nameof(phoneNumber));
    }

    Name = name;
    PhoneNumber = phoneNumber;
    Email = email;
    Organization = organization;
  }

  public string Name { get; private init; }
  public string? Organization { get; private init; }
  public int PhoneNumber { get; private init; }
  public string Email { get; private init; }
}