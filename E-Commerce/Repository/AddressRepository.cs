using E_Commerce.Models;
using System.Net;

namespace E_Commerce.Repository
{
    public class AddressRepository:IAddressRepository
    {
        E_Context context;
        public AddressRepository(E_Context _context)
        {
            _context = context;
        }
        public void Add(Address address)
        {
            context.Addresses.Add(address);
        }
        public void Update(Address address)
        {
            context.Addresses.Update(address);
        }
        public void Delete(long id)
        {
            Address address = GetById(id);
            if (address != null)
            {
                context.Addresses.Attach(address);
                context.Addresses.Remove(address);
            }
        }
        public Address GetById(long id)
        {
            return context.Addresses.FirstOrDefault(e => e.Id == id);

        }

        public List<Address> GetAll()
        {
            return context.Addresses.ToList();
        }

        public void save()
        {
            context.SaveChanges();
        }


    }
}
