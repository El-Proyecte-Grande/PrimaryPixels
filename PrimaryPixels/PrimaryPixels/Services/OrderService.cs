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

    public async Task<IEnumerable<OrderDetails>> GetOrdersByUserId(string id)
    {
        // Get every order by userId
        var orders = await _orderRepository.GetOrdersByUserId(id);
        // Get every corresponding orderDetails for these orders, run them in parallel.
        var orderDetailsTasks = orders
            .Select(order => _orderDetailsRepository.GetProductsForOrder(order.Id));
        var orderDetailsLists = await Task.WhenAll(orderDetailsTasks);
        // return every orderDetails for this user in a collection.
        return orderDetailsLists.SelectMany(details => details);
    }
} 