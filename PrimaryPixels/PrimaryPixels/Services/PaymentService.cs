using PrimaryPixels.Contracts;
using Stripe;

namespace PrimaryPixels.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> IsPaymentSuccessful(PaymentRequest request)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(request.Amount * 100),
                Currency = request.Currency,
                Source = request.Token,
                Description = "Payment example"
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);
            return charge.Status == "succeeded";
        }
    }
}
