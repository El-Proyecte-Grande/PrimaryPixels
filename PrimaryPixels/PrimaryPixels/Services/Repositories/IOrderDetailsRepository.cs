using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services.Repositories;

public interface IOrderDetailsRepository
{
    public Task<IEnumerable<OrderDetails>> GetProductsForOrder(int orderId);
    public abstract Task<int> Add(OrderDetails entity);
}