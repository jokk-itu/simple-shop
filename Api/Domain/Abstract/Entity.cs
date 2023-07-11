using System;

namespace Api.Domain.Abstract;

#pragma warning disable S4035 // The responsibility of equality comparison is immutable
public abstract class Entity : IEquatable<Entity>
#pragma warning restore S4035
{
  public int Id { get; private set; }

  public bool IsTransient()
  {
    return Id == default;
  }

  public bool Equals(Entity? other)
  {
    return Equals((object?)other);
  }

  public override bool Equals(object? obj)
  {
    return obj is Entity entity
           && entity.GetType() == GetType()
           && !entity.IsTransient()
           && !IsTransient()
           && Id == entity.Id;
  }

  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }

  public static bool operator ==(Entity left, Entity right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(Entity left, Entity right)
  {
    return !left.Equals(right);
  }
}