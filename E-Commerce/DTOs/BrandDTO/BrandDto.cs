using E_Commerce.DTOs.ProductDTO;

namespace E_Commerce.DTOs.BrandDTO
{
    public class BrandDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
        public List<ProductListDto> Products { get; set; }
    }
}
