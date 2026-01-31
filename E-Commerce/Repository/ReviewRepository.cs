using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        E_Context context;
        public ReviewRepository(E_Context _context)
        {
            context = _context;
        }

        public void Add(Review review)
        {
            context.Reviews.Add(review);

        }
        public Review GetById(long id)
        {
            return context.Reviews
                .Include(R => R.User)
                .Include(R => R.Product)
                .FirstOrDefault(e => e.Id == id);
        }
        public void Update(Review review)
        {
            context.Reviews.Update(review);
        }
        public void Delete(long id)
        {
            Review review = GetById(id);
            if (review != null)
            {
                context.Reviews.Remove(review);
            }
        }
        public IQueryable<Review> GetAll()
        {
            return context.Reviews
                .Include(R => R.User)
                .Include(R => R.Product);
        }

        public void save()
        {
            context.SaveChanges();
        }


    }
}
