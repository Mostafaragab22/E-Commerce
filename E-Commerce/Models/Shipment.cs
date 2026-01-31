namespace E_Commerce.Models
{
    public class Shipment : BaseEntity
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }

        public string CarrierName { get; set; }
        public string TrackingNumber { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string Address { get; set; }

        public string ShippingLabelUrl { get; set; }
    }

}