using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface ICartRepository
    {
        public void Add(Cart cart);
        public void Update(Cart cart);
        public void Delete(long id);
        public IQueryable<Cart> GetAll();
        public void save();
        public Cart GetById(long id);
    }
}
