using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class NotificationRepository:INotificationRepository

    {
        E_Context context {  get; set; }
        public NotificationRepository(E_Context _context)
        {
            context = _context;
        }
        public List<Notification> GetByUserId(long userId)
        {
            return context.Notifications
             .Where(n => n.UserId == userId)
             .OrderByDescending(n => n.CreatedAt)
             .ToList();
        }
        public Notification GetById(long id)
        {
            return context.Notifications.Find(id);
        }

        public void Add(Notification notification)
        {
            context.Notifications.Add(notification);
        }

        public void Update(Notification notification)
        {
            context.Notifications.Update(notification);
        }

        public void save()
        {
            context.SaveChanges();
        }


    }
}
