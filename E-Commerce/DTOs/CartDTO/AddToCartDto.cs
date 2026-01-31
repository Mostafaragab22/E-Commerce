namespace E_Commerce.DTOs.CartDTO
{
    public class AddToCartDto
    {
        public long ProductId { get; set; }
        public long? VariantId { get; set; }
        public int Quantity { get; set; }
    }
}
