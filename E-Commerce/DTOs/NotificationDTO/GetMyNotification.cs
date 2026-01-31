namespace E_Commerce.DTOs.NotificationDTO
{
    public class GetMyNotification
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }


    }
}
