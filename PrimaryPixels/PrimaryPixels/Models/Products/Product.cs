namespace PrimaryPixels.Models.Products;

public abstract class Product
{
    public int Id { get; init; }
    public int Price { get; init; }
    public bool Availability { get; init; }
    public string Name { get; init; }
    public int Sold { get; init; }
    public int TotalSold { get; init; }
}