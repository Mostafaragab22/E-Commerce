namespace E_Commerce.Models
{
    public class Cart : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long? SessionId { get; set; }
        public decimal TotalAmount { get; set; } = 0;

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}