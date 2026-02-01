using E_Commerce.DTOs.OrderDTO;
using E_Commerce.DTOs.Orders;
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
    public class OrderController : ControllerBase
    {
        IOrderRepository OrderRepository { get; set; }
        IInventoryRepository InventoryRepository { get; set; }
        public OrderController(IOrderRepository orderRepository, IInventoryRepository inventoryRepository)
        {
            OrderRepository = orderRepository;
            InventoryRepository = inventoryRepository;
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateOrder(CreateOrderDto orderDto)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invaild User");
            var order = new Order
            {
                UserId = userId,
                OrderNumber = "ORD-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                ShippingAddressId = orderDto.ShippingAddressId,
                BillingAddressId = orderDto.BillingAddressId,
                PaymentMethod = orderDto.PaymentMethod,
                Notes = orderDto.Notes,
                Status = OrderStatus.Pending,
            };
            OrderRepository.Add(order);

            foreach (var itemDto in order.OrderItems)
            {
                var inventoryItem = InventoryRepository.GetByItem(itemDto.ProductId, "Product");
                if (inventoryItem == null || inventoryItem.AvailableQuantity < itemDto.Quantity)
                    return BadRequest($"Not enough inventory for product {itemDto.ProductId}");
                inventoryItem.Quantity -= itemDto.Quantity;
                InventoryRepository.Update(inventoryItem);


                var movement = new InventoryMovement
                {
                    ItemType = "Product",
                    ItemId = itemDto.ProductId,
                    MovementType = "Decrease",
                    QuantityChange = itemDto.Quantity,
                    ReferenceType = "Order",
                    ReferenceId = order.Id


                };
                InventoryRepository.Add(movement);
            }

                OrderRepository.save();
                return CreatedAtAction(nameof(GetMyOrder), new { id = order.Id }, order);

            }

            [HttpGet("{id}")]
        
            public IActionResult GetMyOrder(long id)
            {
                var userIdString = User.UserId();
                if (!long.TryParse(userIdString, out var userId))
                    return BadRequest("Invaild User");

                var order = OrderRepository.GetById(id);
                if (order == null)
                    return NotFound();
                if (order.UserId != userId)
                    return Forbid();

                var orderDto = new OrderDetailsDto
                {
                    Id = order.Id,
                    OrderNumber = order.OrderNumber,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount ?? 0,
                    PaymentStatus = order.PaymentStatus,

                    Items = order.OrderItems.Select(o => new OrderItemDto
                    {
                        ProductId = o.ProductId,
                        VariantId = o.VariantId,
                        ProductName = o.Product.Name,
                        Quantity = o.Quantity,
                        UnitPrice = o.UnitPrice
                    }).ToList()

                };


                return Ok(orderDto);
            }
            [HttpGet("OrderDetails/{id}")]
            [Authorize]
            public async Task<IActionResult> OrderDetails(long id)
            {
                var userIdString = User.UserId();
                if (!long.TryParse(userIdString, out var userId))
                    return BadRequest("Invaild User");
                var orderDetial = OrderRepository.GetById(id);
                if (orderDetial == null)
                    return NotFound();
                if (orderDetial.UserId != userId)
                    return Forbid();

                var OrderDetials = new OrderListDto()
                {

                    Id = orderDetial.Id,
                    OrderNumber = orderDetial.OrderNumber,
                    Status = orderDetial.Status,
                    TotalAmount = orderDetial.TotalAmount ?? 0,
                    PaymentStatus = orderDetial.PaymentStatus,
                    Items = orderDetial.OrderItems.Select(i => new OrderItemDto
                    {
                        ProductId = i.ProductId,
                        VariantId = i.VariantId,
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice

                    }).ToList(),

                };
                return Ok(OrderDetials);
            }
        [HttpPut("CancelOrder/{id}")]
        [Authorize]
        public IActionResult CancelOrder(long id) 
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return Unauthorized(); 

            var order = OrderRepository.GetById(id);
            if (order == null)
                return NotFound("Order not found");

            if (order.UserId != userId)
                return Forbid();

            
            if (order.Status == OrderStatus.Shipped ||
                order.Status == OrderStatus.Delivered ||
                order.Status == OrderStatus.Cancelled)
            {
                return BadRequest("Order cannot be cancelled in its current state");
            }

            order.Status = OrderStatus.Cancelled;

            
            foreach (var item in order.OrderItems)
            {
                var invetoryItem = InventoryRepository.GetByItem(item.ProductId, "Product");
                if (invetoryItem != null)
                {
                    invetoryItem.Quantity += item.Quantity;
                    InventoryRepository.Update(invetoryItem);
                }

                var movement = new InventoryMovement
                {
                    ItemType = "Product",
                    ItemId = item.ProductId,
                    MovementType = "Increase",
                    QuantityChange = item.Quantity,
                    ReferenceType = "OrderCancellation",
                    ReferenceId = order.Id
                };
                InventoryRepository.Add(movement);
            }

            OrderRepository.Update(order);
            OrderRepository.save();
            InventoryRepository.save(); 

            return Ok("Order cancelled and inventory updated");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<OrderListDto>>> GetAllOrders()
        {
            var Orders = OrderRepository.GetAllOrder()

               .Select(d => new OrderListDto
               {
                   Id = d.Id,
                   OrderNumber = d.OrderNumber,
                   Status = d.Status,
                   TotalAmount = d.TotalAmount ?? 0




               }).ToList();
                return Ok(Orders);

            }
            [HttpPut("AdminUpdateStuts/{id}")]
            [Authorize(Roles = "Admin")]
            public IActionResult UpdateSatus(UpdateOrderStatusDto orderStatusDto, long id)
            {
                var order = OrderRepository.GetById(id);
                if (order == null)
                    return NotFound();

                order.Status = orderStatusDto.NewStatus;

                OrderRepository.Update(order);
                OrderRepository.save();
                return Ok();

            }
        }

    } 
