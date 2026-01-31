namespace E_Commerce.Models
{
    public class Wishlist : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }

}
