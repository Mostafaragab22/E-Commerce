using E_Commerce.DTOs.CartDTO;
using E_Commerce.Extensions;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartRepository CartRepository { get; set; }
        ICartItemRepository CartItemRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        public CartController (ICartRepository cartRepository, IProductRepository productRepository , ICartItemRepository cartItemRepository)
        {
            CartRepository = cartRepository;
            ProductRepository = productRepository;
            CartItemRepository = cartItemRepository;
        }

        [HttpGet]
        public ActionResult<List<CartDto>> GetCart()
        {
            var cart = CartRepository.GetAll()
                .Select(cart => new CartDto {

                    
                    CartItems = cart.CartItems.Select(cartItem => new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductName = cartItem.Product.Name,
                        VariantAttributes = cartItem.ProductVariant.Attributes,
                        UnitPrice = cartItem.UnitPrice,
                        Quantity = cartItem.Quantity
                          

                    }).ToList(),

                }).ToList();

            return Ok(cart);

        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(AddToCartDto dto)
        { 
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid user id");

            var cart = CartRepository.GetById(userId) ?? new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem>()
            };

            if (cart.Id == 0)
                CartRepository.Add(cart);

            
            var product = ProductRepository.GetById(dto.ProductId);
            if (product == null)
                return NotFound("Product not found");

            var item = cart.CartItems.FirstOrDefault(ci =>
                ci.ProductId == dto.ProductId && ci.VariantId == dto.VariantId);

            if (item != null)
                item.Quantity += dto.Quantity;
            else
                cart.CartItems.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    VariantId = dto.VariantId,
                    Quantity = dto.Quantity,
                    UnitPrice = product.SalePrice
                });

            CartRepository.save();

            return Ok("Item added to cart");
        }

      
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateCart(long id, [FromBody]UpdateCartItemDto dto)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid user");

            var cartItem = CartItemRepository.GetById(id);
            if (cartItem == null)
                return NotFound();

            if (cartItem.Cart.UserId != userId)
                return Forbid();

            cartItem.Quantity = dto.Quantity;

            CartItemRepository.Update(cartItem);
            CartItemRepository.Save();

            return Ok("Cart item updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCart(long id)
        {
            
                var userIdString = User.UserId();
                if (!long.TryParse(userIdString, out var userId))
                    return BadRequest("Invalid user");
                var cartitem = CartItemRepository.GetById(id);
                if (cartitem == null)
                    return NotFound();
            if (cartitem.Cart.UserId != userId)
                return Forbid();

            CartItemRepository.Delete(id);
            CartItemRepository.Save();

            return Ok("Cart item deleted");




        }
    }
}
