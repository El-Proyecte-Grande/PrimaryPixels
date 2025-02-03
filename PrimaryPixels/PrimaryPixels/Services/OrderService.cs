using PrimaryPixels.DTO;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Services;

public class OrderService : IOrderService
{
    protected IOrderRepository _orderRepository;
    protected IProductRepository _productRepository;
    protected IOrderDetailsRepository _orderDetailsRepository;
    public OrderService(IOrderRepository repository, IProductRepository productRepository, IOrderDetailsRepository orderDetailsResRepository)
    {
        _orderRepository = repository;
        _productRepository = productRepository;
        _orderDetailsRepository = orderDetailsResRepository;
    }

    public async Task<int> CreateOrder(OrderDTO orderDto, string userId)
    {
        // Create the real orderDetails without orderId and calculate the whole order's price
        var orderDetails = CreateOrderDetails(orderDto.OrderProducts);
        // Sum the whole order's price
        var price = await SumPrice(orderDetails);
        // Instance of Order class with the new price, store this in the DB
        var order = new Order()
        {
            UserId = userId, Address = orderDto.Address, City = orderDto.City, FirstName = orderDto.FirstName,
            LastName = orderDto.LastName, OrderDate = DateOnly.FromDateTime(DateTime.Now), Price = price
        };
        // POST the real order instance 
        int orderId = await _orderRepository.Add(order);
        // Add orderId to every orderDetails
        await foreach (var orderDetail in orderDetails)
        {
            orderDetail.OrderId = orderId;
            await _orderDetailsRepository.Add(orderDetail);
        }
        return orderId;
    }

    // Create the real orderDetails
    private async IAsyncEnumerable<OrderDetails> CreateOrderDetails(List<OrderDetailsDTO> orderDetailsDtos)
    {
        foreach (var orderDetailDto in orderDetailsDtos)
        {
            var product = await _productRepository.GetById(orderDetailDto.ProductId);
            if (product == null)
            {
                throw new Exception($"Product with ID {orderDetailDto.ProductId} not found.");
            }
            yield return new OrderDetails()
            {
                ProductId = orderDetailDto.ProductId,
                Quantity = orderDetailDto.Quantity, UnitPrice = product.Price
            };
        }
    }
    private async Task<int> SumPrice(IAsyncEnumerable<OrderDetails> orderDetails)
    {
        int price = 0;
        await foreach (var detail in orderDetails)
        {
            price += detail.TotalPrice;
        }
        return price;
    }

    
    public async Task<IEnumerable<OrderDetailsResponseDTO>> GetOrdersByUserId(string id)
    {
        // Get orders by userId
        var orders = await _orderRepository.GetOrdersByUserId(id);
        var orderDetailsResponse = new List<OrderDetailsResponseDTO>();
        // Get every corresponding orderDetail for these orders and create the OrderDetailResponseDTOs
        foreach (var order in orders)
        {
            var orderDetailsForOrder = await _orderDetailsRepository.GetProductsForOrder(order.Id);
            // Create every orderDetailsDTO, including a ProductDTO instead of Product
            var orderDetailsDTOsForOrder = orderDetailsForOrder.Select(od =>
            {
               return new OrderDetailsResponseDTO()
                {
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    OrderId = od.OrderId,
                    Product = new ProductDTO()
                    {
                        Image = od.Product.Image,
                        Price = od.Product.Price,
                        Name = od.Product.Name
                    }
                };
            });
            // Push every orderDetailsDTO to this collection
            orderDetailsResponse.AddRange(orderDetailsDTOsForOrder); 
        }
        return orderDetailsResponse;
    }
} 