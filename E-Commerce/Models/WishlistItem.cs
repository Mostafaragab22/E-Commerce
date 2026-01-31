namespace E_Commerce.Models
{
    public class WishlistItem : BaseEntity
    {
        public long WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long? VariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }
    }

}
