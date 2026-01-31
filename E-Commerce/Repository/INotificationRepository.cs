using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface INotificationRepository
    {
        public void Update(Notification notification);
        public List<Notification> GetByUserId (long userId);
        public void Add(Notification notification);
        public void save();
        public Notification GetById(long userId);
    }
}
