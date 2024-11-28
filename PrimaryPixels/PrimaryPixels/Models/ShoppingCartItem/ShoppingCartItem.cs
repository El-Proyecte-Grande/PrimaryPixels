using System.Text.Json.Serialization;
using PrimaryPixels.Models.Products;

namespace PrimaryPixels.Models.ShoppingCartItem;

public class ShoppingCartItem
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public int UnitPrice { get; init; }
    public int TotalPrice => UnitPrice * Quantity;
}