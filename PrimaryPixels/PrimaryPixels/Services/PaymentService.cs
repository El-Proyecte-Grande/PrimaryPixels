using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace PrimaryPixels.Services;

public class PaymentService : IPaymentService
{
    
    private readonly IEmailSender _emailSender;

    public PaymentService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    public async Task SendEmail(int orderId, string email)
    {
        string message = $"Thank you for your order. Your order number is: {orderId}";
        await _emailSender.SendEmailAsync(email, "Order Confirmation", message);
    }
}