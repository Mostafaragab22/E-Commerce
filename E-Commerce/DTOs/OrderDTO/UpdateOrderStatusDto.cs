using E_Commerce.Models;

namespace E_Commerce.DTOs.OrderDTO
{
    public class UpdateOrderStatusDto
    {
        public OrderStatus NewStatus { get; set; } 
    }
}
