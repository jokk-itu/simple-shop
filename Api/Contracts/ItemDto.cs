namespace Api.Contracts;

public class ItemDto
{
  public int Id { get; init; }
  public string Name { get; init; } = null!;
  public decimal Price { get; init; }
}