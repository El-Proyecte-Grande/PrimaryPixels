using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Contracts;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services;
using PrimaryPixels.Services.Repositories;
using Stripe;

namespace PrimaryPixels.Controllers;

[ApiController]
public class PaymentController : ControllerBase
{

    private readonly StripeClient _stripeClient;
    
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;
    
    public PaymentController(IConfiguration configuration, IPaymentService paymentService, IUserRepository userRepository, IOrderRepository orderRepository)
    {
        _stripeClient = new StripeClient(configuration["StripeSecretKey"]);
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    [HttpPost("/api/Create-Payment-Intent"), Authorize]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequest request)
    {
        try
        {
            var order = await _orderRepository.GetById(request.OrderId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated properly");
            }
            if (order.UserId != userId)  return Forbid();
            if (order.PaymentStatus == PaymentStatus.Paid) return BadRequest("Order is already paid!");
            var options = new PaymentIntentCreateOptions()
            {
                Amount = order.Price,
                Currency = "HUF",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = await service.CreateAsync(options);
            order.PaymentIntentId = paymentIntent.Id;
            await _orderRepository.Update(order);
            return Ok(paymentIntent.ClientSecret);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // After order successfully paid, this method change order's payment status and sends and email.
    [HttpPost("/api/payments/success"), Authorize]
    public async Task<IActionResult> SuccessPayment([FromBody] PaymentRequest request)
    {
        try
        {
            var order = await _orderRepository.GetById(request.OrderId);
            order.PaymentStatus = PaymentStatus.Paid;
            await _orderRepository.Update(order);
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId not found.");
            }
            var user = await _userRepository.GetUserById(userId);
            var email = user.Email;
            await _paymentService.SendEmail(request.OrderId, email);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}