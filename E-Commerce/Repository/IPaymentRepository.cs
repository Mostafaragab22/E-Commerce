using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IPaymentRepository
    {
        public void Add(Payment payment);
        public void Update(Payment payment);
        public void Delete(long id);
        public Payment GetById(long id);
        
        public void save ();
        public List<Payment> GetByUserId(long userId);
    }
}
