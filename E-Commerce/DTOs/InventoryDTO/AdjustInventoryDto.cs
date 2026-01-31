namespace E_Commerce.DTOs.InventoryDTO
{
    public class AdjustInventoryDto
    {
        public string ItemType { get; set; }
        public long ItemId { get; set; }
        public int QuantityChange { get; set; }
        public string Reason { get; set; }
    }
}
