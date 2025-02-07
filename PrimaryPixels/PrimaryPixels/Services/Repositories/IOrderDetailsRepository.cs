using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services.Repositories;

public interface IOrderDetailsRepository
{
    public Task<IEnumerable<OrderDetails>> GetProductsForOrder(int orderId);
    public Task<int> Add(OrderDetails entity);
    public Task<IEnumerable<OrderDetails>> GetAll();
    public Task<OrderDetails> GetById(int id);
    public Task<int> Update(OrderDetails entity);
    public Task<int> DeleteById(int id);
}