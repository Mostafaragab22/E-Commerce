namespace E_Commerce.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }


        public ICollection<Product> Products { get; set; } = new List<Product>();
        
    }
}
