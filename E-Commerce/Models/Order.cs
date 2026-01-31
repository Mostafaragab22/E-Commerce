namespace E_Commerce.Models
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public long? UserId { get; set; }
        public User User { get; set; }

        public OrderStatus Status { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal? TotalAmount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }

        public long ShippingAddressId { get; set; }
        public long BillingAddressId { get; set; }

        public string? Notes { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
        public ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();
    }

    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }

}