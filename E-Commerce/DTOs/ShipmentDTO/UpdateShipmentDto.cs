using E_Commerce.Models;

namespace E_Commerce.DTOs.ShipmentDTO
{
    public class UpdateShipmentDto
    {
        public OrderStatus Status { get; set; } 
        public DateTime? DeliveredAt { get; set; }
    }
}
