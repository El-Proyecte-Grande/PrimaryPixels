using PrimaryPixels.DTO;
using PrimaryPixels.Models.Order;

namespace PrimaryPixels.Services;

public interface IOrderService
{
    Task<int>CreateOrder(OrderDTO orderDto, string userId);
    Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserId(string userId);
}