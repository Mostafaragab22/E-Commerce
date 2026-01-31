using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class WishlistRepository:IWishlistRepository
    {
        E_Context context;
        public WishlistRepository(E_Context _context)
        {
            context = _context;
        }
        public void Add(Wishlist wishlist)
        {
            context.Wishlists.Add(wishlist);
        }
        public void Update(Wishlist wishlist)
        {
            context.Wishlists.Update(wishlist);
        }
        public void Delete(long id)
        {
            Wishlist wishlist = GetById(id);
            if (wishlist != null)
            {

                context.Wishlists.Remove(wishlist);
            }
        }
        public Wishlist GetByUserId(long userId)
        {
            return context.Wishlists
                .Include(w => w.Items)
                .FirstOrDefault(w => w.UserId == userId);
        }
        public Wishlist GetById(long id)
        {
            return context.Wishlists
                .Include(C => C.Items)
                .Include(C => C.User)
            .FirstOrDefault(e => e.Id == id);

        }

        public IQueryable<Wishlist> GetAll()
        {
            return context.Wishlists
                .Include(c => c.Items)
              .Include(c => c.User)
               ;
        }

        public void save()
        {
            context.SaveChanges();
        }
    }
}
