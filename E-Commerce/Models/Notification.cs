namespace E_Commerce.Models
{
    public class Notification : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public string ReferenceType { get; set; }
        public long? ReferenceId { get; set; }
    }
}
