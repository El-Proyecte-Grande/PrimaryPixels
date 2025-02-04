namespace PrimaryPixels.DTO;

public class OrderDetailsResponseDTO
{
    public ProductDTO Product { get; init; }
    public int Quantity { get; init; }
    public int UnitPrice { get; init; }
    public int OrderId { get; init; }
    public DateOnly OrderDate { get; init; }
    public int TotalPrice => Quantity * UnitPrice;
}