namespace E_Commerce.DTOs.Orders
{
    public class CreateOrderDto
    {

        public long ShippingAddressId { get; set; }
        public long BillingAddressId { get; set; }
        public string PaymentMethod { get; set; }
        public string? Notes { get; set; }

    }
}
 
