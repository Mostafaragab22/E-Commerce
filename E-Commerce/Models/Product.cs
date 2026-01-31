namespace E_Commerce.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Sku { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public long BrandId { get; set; }
        public Brand Brand { get; set; }

        public decimal BasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? TaxRate { get; set; }

        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDigital { get; set; }

        public string MainImage { get; set; }
        public string? GalleryImage { get; set; }
        public string ?SeoTitle { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
