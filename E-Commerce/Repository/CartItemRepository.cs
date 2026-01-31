using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class CartItemRepository:ICartItemRepository
    {
        E_Context context;
        public CartItemRepository(E_Context _context)
        {
            _context = context;
        }
        public CartItem GetById(long id)
        {
            return context.CartItems.FirstOrDefault(e => e.Id == id);

        }

        public void Update(CartItem item)
        {
            context.CartItems.Update(item);
        }

        public void Delete(long id)
        {
            CartItem cartitem = GetById(id);
            if (cartitem != null)
            {

                context.CartItems.Remove(cartitem);
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
