namespace E_Commerce.DTOs.RefundDTO
{
    public class RefundResponseDto
    {
        public long RefundId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
