using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        E_Context context;
        public  ProductRepository(E_Context _context)
        {
            context = _context;
        }
        public void Add(Product prod)
        {
            context.Products.Add(prod);

        }
        public void Delete(long id)
        {
            Product prod = GetById(id);
            if (prod != null)
            {
                context.Products.Remove(prod);
            }
        }

        public IQueryable<Product> GetAllProduct()
        {
            return context.Products
                          .Include(p => p.Category)
                          .Include(p => p.Brand);
        }


        public Product GetById(long id)
        {
            return context.Products
                  .Include(p => p.Category)
                  .Include(p => p.Brand)
                  .FirstOrDefault(e => e.Id == id);
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void Update(Product prod)
        {
            context.Products.Update(prod);
        }

    }

}
