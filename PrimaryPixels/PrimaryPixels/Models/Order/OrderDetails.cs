using PrimaryPixels.Models.Products;

namespace PrimaryPixels.Models.Order;

public class OrderDetails
{
    public int Id { get; init; }
    public int OrderId { get; set; }
    public int ProductId { get; init; }
    public Product? Product { get; set; }
    public int Quantity { get; init; }
    public int UnitPrice { get; init; }
    public int TotalPrice => Quantity * UnitPrice;
}