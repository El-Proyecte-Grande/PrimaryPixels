using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
    public Task<int> Add(Order entity);
    public Task<Order> GetById(int id);
    public Task<int> Update(Order order);
}