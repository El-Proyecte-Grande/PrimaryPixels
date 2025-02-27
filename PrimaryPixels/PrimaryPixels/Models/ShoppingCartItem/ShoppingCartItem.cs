using System.Text.Json.Serialization;
using PrimaryPixels.Models.Products;

namespace PrimaryPixels.Models.ShoppingCartItem;

public class ShoppingCartItem
{
    public int Id { get; init; }
    public string UserId { get; init; }
    public int ProductId { get; init; }
    public Product? Product { get; init; }
    public int Quantity { get; init; }
    public int UnitPrice => Product?.Price ?? 0;
    public int TotalPrice => UnitPrice * Quantity;
}
