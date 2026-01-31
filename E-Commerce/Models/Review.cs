namespace E_Commerce.Models
{
    public class Review : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public OrderStatus Status { get; set; } 

       
    }

    
}
