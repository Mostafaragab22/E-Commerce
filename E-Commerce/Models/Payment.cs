namespace E_Commerce.Models
{
    public class Payment : BaseEntity
    {

        public long UserId { get; set; }
        public User User { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }

        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus Status { get; set; }

        public string TransactionReference { get; set; }
        public string GatewayResponse { get; set; }

        public DateTime PaidAt { get; set; }

        public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
    }

}
