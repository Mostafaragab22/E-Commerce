using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public class UserRepository:IUserRepository
    {
        E_Context context;
        public UserRepository(E_Context _context)
        {
            context = _context;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
        }
        public void Update(User user)
        {
            context.Users.Update(user);
        }

        public void Delete(long id)
        {
            User user = GetById(id);
            context.Users.Attach(user);
            context.Users.Remove(user);

           
        }

        public User GetById(long id)
        {
            return context.Users.FirstOrDefault(e => e.Id == id);
        }
        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public void save()
        {
            context.SaveChanges();
        }


    }

}
