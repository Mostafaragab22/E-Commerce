namespace E_Commerce.DTOs.ProductDTO
{
    public class ProductDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDigital { get; set; }
        public string MainImage { get; set; }
    }
}
