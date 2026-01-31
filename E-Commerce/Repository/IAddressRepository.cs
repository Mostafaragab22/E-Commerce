using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IAddressRepository
    {
        public void Add(Address address);
        public void Update(Address address);
        public void Delete(long id);
        public List<Address> GetAll();
        public void save();
        public Address GetById(long id);
    }
}
