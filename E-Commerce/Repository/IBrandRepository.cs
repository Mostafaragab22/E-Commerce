using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IBrandRepository
    {
        public void Add(Brand brand);
        public void Update(Brand brand);
        public void Delete(long id);
        public IQueryable<Brand> GetAll();
        public void save();
        public Brand GetById(long id);
    }
}
