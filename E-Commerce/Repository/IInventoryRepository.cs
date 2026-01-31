using E_Commerce.Models;
using Microsoft.Build.Tasks;

namespace E_Commerce.Repository
{
    public interface IInventoryRepository
    {
        public void Add(InventoryMovement movement);
        public void Add(Inventory inventory);
        public void Update(Inventory inventory);
        public void Delete(long id);
        public List<Inventory> GetAll();
        public void save();
        public Inventory GetById(long id);
        public Inventory GetByItem(long itemId,string itemType);
        public List<InventoryMovement> GetMovements(string itemType, long itemId);
    }
}
