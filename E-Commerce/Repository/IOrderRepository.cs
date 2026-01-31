using E_Commerce.Models;
using System.Security.Permissions;

namespace E_Commerce.Repository
{
    public interface IOrderRepository
    {
       public void Add(Order order);
       public void Update(Order order);
       public void Delete(long id);
        public Order GetById(long id);
        public IQueryable<Order> GetAllOrder();
        public void save();


    }
}
