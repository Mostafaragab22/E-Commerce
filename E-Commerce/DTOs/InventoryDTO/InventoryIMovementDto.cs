namespace E_Commerce.DTOs.InventoryDTO
{
    public class InventoryMovementDto
    {
        public long Id { get; set; }
        public string ItemType { get; set; }
        public long ItemId { get; set; }
        public string MovementType { get; set; }
        public decimal QuantityChange { get; set; }
        public string ReferenceType { get; set; }
        public long ReferenceId { get; set; }
       
    }
}
