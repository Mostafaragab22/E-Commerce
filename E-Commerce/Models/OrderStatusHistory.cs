namespace E_Commerce.Models
{

    public class OrderStatusHistory : BaseEntity
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }

        public string OldStatus { get; set; }
        public string NewStatus { get; set; }

        public long ChangedByUserId { get; set; }
        public User ChangedByUser { get; set; }

        public string Notes { get; set; }
    }
}


