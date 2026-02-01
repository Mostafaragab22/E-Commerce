using E_Commerce.DTOs.WishlistDTO;
using E_Commerce.Extensions;
using E_Commerce.Models;
using E_Commerce.Repository;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        IWishlistRepository WishlistRepository { get; set; }

        public WishlistController(IWishlistRepository wishlistRepository)
        {
            WishlistRepository = wishlistRepository;
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddToWishlist(AddToWishlistDto wishlistDto)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid User");

            var wishlist = WishlistRepository.GetByUserId(userId);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId,
                    Items = new List<WishlistItem>()
                };
                WishlistRepository.Add(wishlist);
            }

            var itemExists = wishlist.Items.Any(i =>
                i.ProductId == wishlistDto.ProductId &&
                i.VariantId == wishlistDto.VariantId);

            if (itemExists)
                return BadRequest("Item already exists in wishlist");

            wishlist.Items.Add(new WishlistItem
            {
                ProductId = wishlistDto.ProductId,
                VariantId = wishlistDto.VariantId
            });

            WishlistRepository.save();

            return Ok("Item added to wishlist successfully");
        }
        [HttpGet("my")]
        [Authorize]
        public IActionResult GetMyWishlist()
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid User");

            var wishlist = WishlistRepository.GetByUserId(userId);
            if (wishlist == null)
                return Ok(new WishlistDto { Items = new List<WishlistItemDto>() });

            var wishlistDto = new WishlistDto
            {
                Items = wishlist.Items
                .Where(i => i.Product != null) 
                .Select(i => new WishlistItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "N/A",
                    VariantId = i.ProductVariant?.Id ?? 0,
                    VariantAttributes = i.ProductVariant?.Attributes
                }).ToList()
            };

            return Ok(wishlistDto);
        }

        [HttpDelete("items/{itemId}")]
        [Authorize]
        public IActionResult RemoveItemFromWishlist(long itemId)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid User");

            var wishlist = WishlistRepository.GetByUserId(userId);
            if (wishlist == null)
                return NotFound("Wishlist not found");

            var item = wishlist.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                return NotFound("Item not found in wishlist");

            wishlist.Items.Remove(item);

            WishlistRepository.save();

            return Ok("Item removed from wishlist");
        }


    }
}
