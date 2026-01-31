using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IShipmentRepository
    {
        public void Add(Shipment shipment);
        public void Update(Shipment shipment);
        public void Delete(long id);
        public void save();
        public IQueryable<Shipment> GetAll();
        public Shipment GetShipment(long id);


    }
}
