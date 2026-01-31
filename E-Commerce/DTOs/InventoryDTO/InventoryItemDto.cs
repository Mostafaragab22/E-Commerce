namespace E_Commerce.DTOs.Inventory
{
    public class InventoryItemDto
    {
        public long Id { get; set; }
        public string ItemType { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }

  
}
