using E_Commerce.DTOs.Inventory;
using E_Commerce.DTOs.InventoryDTO;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        InventoryRepository InventoryRepository { get; set; }

        public InventoryController(InventoryRepository inventoryRepository)
        {
            InventoryRepository = inventoryRepository;
        }
        [Authorize (Roles = "Admin")]
        [HttpGet]
        public IActionResult GetInventory()
        {
            var inventory = InventoryRepository.GetAll()
                .Select(I => new InventoryItemDto
            {
                Id = I.Id,
                ItemId = I.Id,  
                Quantity = I.Quantity,
                ItemType =I.ItemType,
                ReservedQuantity = I.ReservedQuantity,
                AvailableQuantity = I.Quantity - I.ReservedQuantity

            }).ToList();
            return Ok(inventory);
        }


        [HttpPost("Adjust")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdjustInventory(AdjustInventoryDto dto)
        {
            var inventoryItem = InventoryRepository.GetByItem(dto.ItemId, dto.ItemType);
            if (inventoryItem == null)
                return NotFound("Inventory item not found");

            
            inventoryItem.Quantity += dto.QuantityChange;

            
            var movement = new InventoryMovement
            {
                ItemType = dto.ItemType,
                ItemId = dto.ItemId,
                MovementType = dto.QuantityChange > 0 ? "Increase" : "Decrease",
                QuantityChange = dto.QuantityChange,
                ReferenceType = dto.Reason,
                ReferenceId = 0 ,
            };

            InventoryRepository.Add(movement);
            InventoryRepository.Update(inventoryItem);
            InventoryRepository.save();

            return Ok(new { message = "Inventory adjusted successfully", inventoryItem });
        }

        [HttpGet("Movements/{itemId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInventoryMovements(long itemId, string itemType)
        {
            var movements = InventoryRepository.GetMovements(itemType,itemId)
                .Select(m => new InventoryMovementDto
                {
                    Id = m.Id,
                    ItemType = m.ItemType,
                    ItemId = m.ItemId,
                    MovementType = m.MovementType,
                    QuantityChange = m.QuantityChange,
                    ReferenceType = m.ReferenceType,
                    ReferenceId = m.ReferenceId
                }).ToList();

            return Ok(movements);
        }
    }
}
