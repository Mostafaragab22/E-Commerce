namespace E_Commerce.Models
{

    public class InventoryMovement : BaseEntity
    {
        public string ItemType { get; set; }
        public long ItemId { get; set; }

        public string MovementType { get; set; }
        public decimal QuantityChange { get; set; }

        public string ReferenceType { get; set; }
        public long ReferenceId { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}

