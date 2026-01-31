namespace E_Commerce.Models
{
    public class Inventory : BaseEntity
    {
        public string ItemType { get; set; }  
        public long ItemId { get; set; }

        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int LowStockThreshold { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}


   