using E_Commerce.DTOs.ReviewDTO;
using E_Commerce.Extensions;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        IReviewRepository  ReviewRepository { get; set; }

        public ReviewController(IReviewRepository reviewRepository)
        {
            ReviewRepository = reviewRepository;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddReview(CreateReviewDto reviewDto)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid User");

            var review = new Review
            {
                UserId = userId,
                ProductId = reviewDto.ProductId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.Now,

            };
            ReviewRepository.Add(review);
            ReviewRepository.save();
            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
         
        }
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetReview(int id)
        {
            var UserIdString = User.UserId();
            if (!long.TryParse(UserIdString, out var userId))
                return BadRequest("Invalid User");
            var review = ReviewRepository.GetById(id);
            if (review == null)
                return NotFound();
            if (review.UserId != userId)
                return Forbid();
            var reviewDto = new ReviewDto

            {
                Id = review.Id,
                UserName = review.User.UserName,
                Rating = review.Rating,
                Comment = review.Comment,
            };
            return Ok(reviewDto);
        }
    }
}
