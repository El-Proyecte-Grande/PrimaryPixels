using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
    public abstract Task<int> Add(Order entity);
}