using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        E_Context context;
        public CategoryRepository(E_Context _context)
        {
            context = _context;
        }
        public void Add(Category category)
        {
            context.Categories.Add(category);
        }
        public void Update(Category category)
        {
            context.Categories.Update(category);
        }
        public void Delete(long id)
        {
            Category category = GetById(id);
                
            if (category != null)
            {

              
                context.Categories.Remove(category);
            }
        }
        public Category GetById(long id)
        {
            return context.Categories
                .Include(C => C.Products)                       
                .FirstOrDefault(C => C.Id == id);

        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories
                  .Include(C => C.Products);
                  
                  


        }

        public void save()
        {
            context.SaveChanges();
        }






    }
}
