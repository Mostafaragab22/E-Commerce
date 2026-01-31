namespace E_Commerce.DTOs.CartDTO
{
    public class CartItemDto
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string VariantAttributes { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }
}
