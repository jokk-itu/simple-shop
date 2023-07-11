namespace Api.Contracts;

public class ContactDto
{
  public string Name { get; init; } = null!;
  public string Email { get; init; } = null!;
  public string? Organization { get; init; }
  public int PhoneNumber { get; init; }
}