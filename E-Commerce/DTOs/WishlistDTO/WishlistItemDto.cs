namespace E_Commerce.DTOs.WishlistDTO
{
    public class WishlistItemDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long VariantId { get; set; }
        public string VariantAttributes { get; set; }
    }

}
