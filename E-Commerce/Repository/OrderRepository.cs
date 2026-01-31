using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class OrderRepository:IOrderRepository
    {
        E_Context context;
        public OrderRepository(E_Context _context)
        {
            context = _context;
        }
        public void Add(Order order)
        {
            context.Orders.Add(order);
        }
        public void Update(Order order)
        {
            context.Orders.Update(order);
        }
        public void Delete(long id)
        {
            Order order = GetById(id);
            if (order != null)
            {
               
                context.Orders.Remove(order);
            }
        }
        public IQueryable<Order> GetAllOrder()
        {
            return context.Orders
                .Include(o => o.OrderItems);
        }
        public Order GetById(long id)
        {
            return context.Orders
                .Include(o => o.OrderItems)
                
                .FirstOrDefault(e => e.Id == id);
        }
        public void save ()
        {
            context.SaveChanges();
        }

    }
}
