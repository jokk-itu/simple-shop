namespace Api.Contracts;

public class OrderItemDto
{
  public int ItemId { get; init; }
  public int OrderId { get; init; }
  public int Quantity { get; init; }
}