using PrimaryPixels.Contracts;

namespace PrimaryPixels.Services
{
    public interface IPaymentService
    {
        Task<bool> IsPaymentSuccessful(PaymentRequest request);
        
    }
}
