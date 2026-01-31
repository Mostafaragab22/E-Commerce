namespace E_Commerce.Models
{
    public class ProductVariant : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public string Sku { get; set; }
        public string Attributes { get; set; }  
        public decimal PriceOverride { get; set; }
        public string Image { get; set; }

        public bool IsActive { get; set; }

      
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
