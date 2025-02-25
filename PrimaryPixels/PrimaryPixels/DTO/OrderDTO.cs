namespace PrimaryPixels.Models.Order;

public class OrderDTO
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Address { get; init; }
    public string City { get; init; }
    public List<OrderDetailsDTO> OrderProducts { get; init; }
    public string? PaymentToken { get; init; }
}