using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IWishlistRepository
    {
        public void Add(Wishlist wishlist);
        public void Update(Wishlist wishlist);
        public void Delete(long id);
        public Wishlist GetById(long id);
        public Wishlist GetByUserId(long userId);

        public IQueryable<Wishlist> GetAll();
        public void save();
    }
}
