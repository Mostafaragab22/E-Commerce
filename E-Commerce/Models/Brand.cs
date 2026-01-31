namespace E_Commerce.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
