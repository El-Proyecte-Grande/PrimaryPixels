using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services;

public interface IOrderService
{
    Task<int>CreateOrder(OrderDTO orderDto, string userId);
    public Task<IEnumerable<OrderDetails>> GetOrdersByUserId(string id);
}