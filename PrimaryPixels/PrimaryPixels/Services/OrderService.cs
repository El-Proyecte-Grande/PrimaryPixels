using PrimaryPixels.Contracts;
using PrimaryPixels.DTO;
using PrimaryPixels.Exceptions;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Services;

public class OrderService : IOrderService
{
    protected IOrderRepository _orderRepository;
    protected IProductRepository _productRepository;
    protected IOrderDetailsRepository _orderDetailsRepository;
    protected IPaymentService _paymentService;
    public OrderService(IOrderRepository repository, IProductRepository productRepository, IOrderDetailsRepository orderDetailsResRepository, IPaymentService paymentService)
    {
        _orderRepository = repository;
        _productRepository = productRepository;
        _orderDetailsRepository = orderDetailsResRepository;
        _paymentService = paymentService;
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
        var paymentRequest = new PaymentRequest()
        {
            Amount = price,
            Token = orderDto.PaymentToken,
            Currency = "HUF"
        };
        if (!await _paymentService.IsPaymentSuccessful(paymentRequest))
        {
            throw new PaymentException("Payment is not successful!!");
        }
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

    public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserId(string userId)
    {
        var orders = await _orderRepository.GetOrdersByUserId(userId);
        return orders.Select(o => new OrderResponseDTO() { Id = o.Id, OrderDate = o.OrderDate, Price = o.Price, City = o.City, Address = o.Address});
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

} 