using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public class PaymentRepository:IPaymentRepository
    {
        E_Context context;
        public PaymentRepository (E_Context _context)
        {
            _context = context;
        }
        public void Add (Payment payment)
        {
            context.Payments.Add(payment);
        }
        public void Update(Payment payment)
        {
            context.Payments.Update(payment);
        }
        public void Delete(long id)
        {
            Payment payment = GetById(id);
            if (payment != null)
            {
                context.Payments.Attach(payment);
                context.Payments.Remove(payment);
            }
        }
        public Payment GetById (long id)
        {
            return context.Payments.FirstOrDefault(e => e.Id == id);

        }

        public List<Payment> GetByUserId(long userId)
        {
            return context.Payments
                .Where(p => p.UserId == userId).ToList();
        }

        public void save()
        {
            context.SaveChanges();
        }
    }
}
