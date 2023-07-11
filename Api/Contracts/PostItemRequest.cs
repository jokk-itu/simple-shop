namespace Api.Contracts;

public class PostItemRequest
{
  public string Name { get; init; } = null!;
  public decimal Price { get; init; }
}