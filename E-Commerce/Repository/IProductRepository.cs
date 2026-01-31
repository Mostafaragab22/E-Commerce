using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IProductRepository
    {
        public void Add(Product Prod);
        public void Update(Product Prod);
        public void Delete(long id);
        public IQueryable<Product> GetAllProduct();
        public Product GetById(long id);
        public void save();
    }
}
