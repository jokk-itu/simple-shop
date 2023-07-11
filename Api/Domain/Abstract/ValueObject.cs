using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Domain.Abstract;

public abstract class ValueObject : IEquatable<ValueObject>
{
  protected abstract IEnumerable<object?> GetEqualityComponents();

  public static bool operator ==(ValueObject left, ValueObject right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(ValueObject left, ValueObject right)
  {
    return !left.Equals(right);
  }

  public bool Equals(ValueObject? other)
  {
    return Equals((object?)other);
  }

  public override bool Equals(object? obj)
  {
    if (obj == null || obj.GetType() != GetType())
    {
      return false;
    }

    var other = (ValueObject)obj;

    return GetEqualityComponents()
      .SequenceEqual(other.GetEqualityComponents());
  }

  public override int GetHashCode()
    => GetEqualityComponents()
      .Select(x => x != null ? x.GetHashCode() : 0)
      .Aggregate((x, y) => x ^ y);
}