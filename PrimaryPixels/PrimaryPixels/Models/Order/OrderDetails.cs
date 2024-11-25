namespace PrimaryPixels.Models.Order;

public class OrderDetails
{
    public int Id { get; init; }
    public int OrderId { get; init; }
    public Order Order { get; init; }
    public int ProductId { get; init; }
    public Product Product { get; init; }
    public int Quantity { get; init; }
    public int UnitPrice { get; init; }
    public int TotalPrice => Quantity * UnitPrice;
}