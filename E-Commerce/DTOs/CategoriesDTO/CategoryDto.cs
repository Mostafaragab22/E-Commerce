using E_Commerce.DTOs.ProductDTO;

namespace E_Commerce.DTOs.Categories
{
    public class CategoryDto
    {
         public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<ProductListDto> Products { get; set; }
    }
}
    
