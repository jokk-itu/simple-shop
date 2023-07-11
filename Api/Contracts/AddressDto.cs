namespace Api.Contracts;

public class AddressDto
{
  public string Street { get; init; } = null!;
  public string City { get; init; } = null!;
  public string? State { get; init; }
  public int ZipCode { get; init; }
}