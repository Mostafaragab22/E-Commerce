using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class ShipmentRepository:IShipmentRepository
    {
        E_Context context;
        public ShipmentRepository(E_Context _context)
        {
            context = _context;
        }
        public void Add(Shipment shipment)
        {
            context.Shipments.Add(shipment);

        }
        public void Update(Shipment shipment)
        {
            context.Shipments.Update(shipment);
        }
        public void Delete(Shipment shipment)
        {
            if (shipment != null)
            {
                context.Shipments.Remove(shipment);
            }

        }
        public void save()
        {
            context.SaveChanges();
        }
        public void Delete(long id)
        {
            Shipment shipment = GetShipment(id);
            if (shipment != null)
            {

                context.Shipments.Remove(shipment);
            }
        }

        public IQueryable<Shipment> GetAll()
        {
            return context.Shipments.Include(i => i.Order);

        }

        public Shipment GetShipment(long id)
        { 
          return context.Shipments.Include(i => i.Order).
                FirstOrDefault(i => i.Id == id);

        }

    }
}
