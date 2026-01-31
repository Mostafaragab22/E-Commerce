namespace E_Commerce.DTOs.ProductDTO
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDigital { get; set; }
        public string MainImage { get; set; }
        public string Barcode { get; set; }
    }
}
