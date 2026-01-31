using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class CartRepository : ICartRepository
    {
        E_Context context;
        public CartRepository(E_Context _context)
        {
            _context = context;
        }
        public void Add(Cart cart)
        {
            context.Carts.Add(cart);
        }
        public void Update(Cart cart)
        {
            context.Carts.Update(cart);
        }
        public void Delete(long id)
        {
            Cart cart = GetById(id);
            if (cart != null)
            {
               
                context.Carts.Remove(cart);
            }
        }
        public Cart GetById(long id)
        {
            return context.Carts
                .Include(C => C.CartItems)
            .FirstOrDefault(e => e.Id == id);

        }

        public IQueryable<Cart> GetAll()
        {
            return context.Carts
              .Include(c => c.CartItems)
               .ThenInclude(ci => ci.Product)
              .Include(c => c.CartItems)
               .ThenInclude(ci => ci.ProductVariant);
        }

        public void save()
        {
            context.SaveChanges();
        }

    }
}
