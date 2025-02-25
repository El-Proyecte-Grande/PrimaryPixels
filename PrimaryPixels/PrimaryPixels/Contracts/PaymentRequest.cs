namespace PrimaryPixels.Contracts
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Token { get; set; }
    }
}
