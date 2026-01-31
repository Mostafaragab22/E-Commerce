namespace E_Commerce.Models
{

    public class Refund : BaseEntity
    {
        public long PaymentId { get; set; }
        public Payment Payment { get; set; }

        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}