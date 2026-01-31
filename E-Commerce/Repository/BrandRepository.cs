using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace E_Commerce.Repository
{
    public class BrandRepository : IBrandRepository
    {
        E_Context context;
        public BrandRepository(E_Context _context)
        {
            context = _context;
        }
        public void Add(Brand brand)
        {
            context.Brands.Add(brand);
        }
        public void Update(Brand brand)
        {
            context.Brands.Update(brand);
        }
        public void Delete(long id)
        {
            Brand brand = GetById(id);
            if (brand != null)
            {
                context.Brands.Attach(brand);
                context.Brands.Remove(brand);
            }
        }
        public Brand GetById(long id)
        {
            return context.Brands
                .Include(b => b.Products)
                .FirstOrDefault(e => e.Id == id);

        }

        public IQueryable<Brand> GetAll()
        {
            return context.Brands
                .Include(b => b.Products);
                
        }

        public void save()
        {
            context.SaveChanges();
        }






    }
}
