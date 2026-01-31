namespace E_Commerce.DTOs.RefundDTO
{
    public class RefundRequestDto
    {
        public long PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; } = null!;
    }
}
