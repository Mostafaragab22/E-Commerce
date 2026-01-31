namespace E_Commerce.DTOs.ProductDTO
{
    public class ProductListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}
