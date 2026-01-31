namespace E_Commerce.DTOs.ReviewDTO
{
    public class ReviewDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
     
    }
}
