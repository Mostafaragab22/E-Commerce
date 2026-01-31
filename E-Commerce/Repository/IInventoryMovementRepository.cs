using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IInventoryMovementRepository
    {
        public void Add(InventoryMovement movement);
        public void Update(InventoryMovement movement);
        public void Delete(long id);
        public List<InventoryMovement> GetAll();
        public void save();
        public InventoryMovement GetById(long id);
    }
}
