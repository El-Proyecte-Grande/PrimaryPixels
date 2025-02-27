namespace PrimaryPixels.DTO;

public class OrderResponseDTO
{
    public int Id { get; init; }
    public DateOnly OrderDate { get; init; }
    public int Price { get; init; }
    public string City { get; init; }
    public string Address { get; init; }
}