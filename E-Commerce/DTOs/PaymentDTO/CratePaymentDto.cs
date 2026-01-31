namespace E_Commerce.DTOs.Payments
{
    public class CreatePaymentDto
    {
        public long OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }

}
