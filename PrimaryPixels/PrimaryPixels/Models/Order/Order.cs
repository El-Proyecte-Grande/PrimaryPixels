namespace PrimaryPixels.Models.Order;

public class Order
{
    public int Id { get; init; }
    public string UserId { get; init; }
    public DateOnly OrderDate { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string City { get; init; }
    public string Address { get; init; }
    public int Price { get; init; }
    public string Name { get; set; }
}