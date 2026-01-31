namespace E_Commerce.DTOs.CartDTO
{
    public class CartDto
    {
      
        public List<CartItemDto> CartItems { get; set; }
        public decimal TotalAmount =>
       CartItems.Sum(i => i.LineTotal);
    }
}
