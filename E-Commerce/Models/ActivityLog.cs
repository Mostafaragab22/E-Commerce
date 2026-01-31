namespace E_Commerce.Models
{
    public class ActivityLog : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public string ActionType { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }

        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }

   
}
