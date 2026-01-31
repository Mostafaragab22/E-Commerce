using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public class InventoryMovementRepository : IInventoryMovementRepository
    {
        E_Context context;
        public InventoryMovementRepository(E_Context _context)
        {
            context = _context;
        }
        public void Add(InventoryMovement movement)
        {
            context.InventoryMovements.Add(movement);
        }
        public void Update(InventoryMovement movement)
        {
            context.InventoryMovements.Update(movement);
        }
        public void Delete(long id)
        {
            InventoryMovement movement = GetById(id);
            if (movement != null)
            {
                context.InventoryMovements.Attach(movement);
                context.InventoryMovements.Remove(movement);
            }

        }
        public InventoryMovement GetById(long id)
        {
            return context.InventoryMovements.FirstOrDefault(e => e.Id == id);

        }

        public List<InventoryMovement> GetAll()
        {
            return context.InventoryMovements.ToList();
        }

        public void save()
        {
            context.SaveChanges();
        }






    }
}
