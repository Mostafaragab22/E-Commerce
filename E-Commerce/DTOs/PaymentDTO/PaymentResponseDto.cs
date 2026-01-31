using E_Commerce.Models;

namespace E_Commerce.DTOs.PaymentDTO
{
    public class PaymentResponseDto
    {
        public long Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string TransactionReference { get; set; }
    }
}
