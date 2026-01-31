using E_Commerce.Models;

namespace E_Commerce.DTOs.OrderDTO
{
    public class OrderListDto
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
