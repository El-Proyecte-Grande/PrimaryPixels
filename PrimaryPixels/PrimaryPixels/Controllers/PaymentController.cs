using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Contracts;
using Stripe;

namespace PrimaryPixels.Controllers;

[ApiController]
public class PaymentController : ControllerBase
{

    private readonly StripeClient _stripeClient;
    
    public PaymentController(IConfiguration configuration)
    {
        _stripeClient = new StripeClient(configuration["StripeSecretKey"]);
    }

    [HttpPost("/api/Create-Payment-Intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequest request)
    {
        try
        {
            var options = new PaymentIntentCreateOptions()
            {
                Amount = request.Amount,
                Currency = "HUF",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = await service.CreateAsync(options);
            return Ok(paymentIntent.ClientSecret);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}