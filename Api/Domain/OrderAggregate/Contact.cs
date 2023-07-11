using System;
using System.Collections.Generic;
using Api.Domain.Abstract;

namespace Api.Domain.OrderAggregate;

public sealed class Contact : ValueObject
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

#pragma warning disable 8618
  public Contact() { }
#pragma warning restore 8618

  public string Name { get; init; }
  public string? Organization { get; init; }
  public int PhoneNumber { get; init; }
  public string Email { get; init; }
  protected override IEnumerable<object?> GetEqualityComponents()
  {
    yield return Name;
    yield return Organization;
    yield return PhoneNumber;
    yield return Email;
  }
}