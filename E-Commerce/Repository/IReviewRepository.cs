using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IReviewRepository
    {
        public void Add(Review review);
        public void Update(Review review);
        public void Delete(long id);
        public Review GetById(long id);
        public IQueryable<Review> GetAll();
        public void save();
    }
}
