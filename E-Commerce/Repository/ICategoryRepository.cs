using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface ICategoryRepository
    {
        public void Add(Category category);
        public void Update(Category category);
        public void Delete(long id);
        public IQueryable<Category> GetAll();
        public void save();
        public Category GetById(long id);
    }
}
