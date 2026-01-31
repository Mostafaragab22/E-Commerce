using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.Repository
{
    public class InventoryRepository: IInventoryRepository
    {
        E_Context context;
        public InventoryRepository (E_Context _context)
        {
            context = _context;
        }
        public void Add(Inventory inventory)
        {
            context.Add(inventory);
        }
        public void Update(Inventory inventory) 
        {
             context.Inventories.Update(inventory);
        }
        public void Delete(long id)
        {
            Inventory inventory = GetById(id);
            if (inventory != null)
            {
                context.Inventories.Attach(inventory);
                context.Inventories.Remove(inventory);
            }

        }
        public Inventory GetById(long id)
        {
            return context.Inventories.FirstOrDefault(e => e.Id == id);

        }

        public List<Inventory> GetAll()
        {
            return context.Inventories.ToList();
        }

        public void save()
        {
            context.SaveChanges();
        }

        public Inventory GetByItem(long itemId,string itemType)
        {
            return context.Inventories.OrderByDescending(m => m.Id)
                .FirstOrDefault(i => i.ItemType == itemType && i.ItemId == itemId);
        }

        public void Add(InventoryMovement movement)
        {
            context.InventoryMovements.Add(movement);
        }
        public List<InventoryMovement> GetMovements(string itemType, long itemId)
        {
            return context.InventoryMovements
                .Where(m => m.ItemType == itemType && m.ItemId == itemId)
                .OrderByDescending(m => m.Id)
                .ToList();
        }
    }
}
