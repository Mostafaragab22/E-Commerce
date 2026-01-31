using E_Commerce.Models;

namespace E_Commerce.DTOs.ShipmentDTO
{
    public class GetShipmentByOrderDto
    {
        public long ShipmentId { get; set; }
        public long OrderId { get; set; }

        public string ShippingCompany { get; set; }
        public string TrackingNumber { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string Address { get; set; }
       

    }
}
