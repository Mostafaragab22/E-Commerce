using E_Commerce.Models;
namespace E_Commerce.Repository
{
    public interface IUserRepository
    {
        public void Add(User user);
        public void Update(User user);
        public void Delete(long id);
        public List<User> GetAll();
        public User GetById(long id);
        public void save();
    }
}
