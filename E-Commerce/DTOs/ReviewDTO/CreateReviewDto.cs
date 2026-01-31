namespace E_Commerce.DTOs.ReviewDTO
{
    public class CreateReviewDto
    {
        public long ProductId { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
    }
}
