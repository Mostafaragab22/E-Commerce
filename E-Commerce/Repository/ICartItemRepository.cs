using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface ICartItemRepository
    {
        public CartItem GetById(long id);
       public void Update(CartItem item);
       public void Delete(long id);
       public void Save();
    }
}
