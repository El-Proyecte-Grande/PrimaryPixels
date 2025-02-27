namespace PrimaryPixels.Services;

public interface IPaymentService
{
    public Task SendEmail(int orderId, string email);
}