using E_Commerce.DTOs.ShipmentDTO;
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
    public class ShipmentController : ControllerBase
    {
        IShipmentRepository ShipmentRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }

        public ShipmentController(IShipmentRepository shipmentRepository, IOrderRepository orderRepository)
        {
            ShipmentRepository = shipmentRepository;
            OrderRepository = orderRepository;
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetShipment(long id)
        {
            
            var shipment = ShipmentRepository.GetShipment(id);
            if (shipment == null)
                return NotFound();
            var shipmentDto = new GetShipmentByOrderDto
            {
                ShipmentId = shipment.Id,
                OrderId = shipment.OrderId,
                ShippingCompany = shipment.CarrierName,
                TrackingNumber = shipment.TrackingNumber,
                Status = shipment.Status,
                ShippedAt = shipment.ShippedAt,
                DeliveredAt = shipment.DeliveredAt,
                Address = shipment.Address,

            };
            return Ok (shipmentDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult CreateShipment(CreateShipmentDto shipmentDto)
        {
            var order = OrderRepository.GetById(shipmentDto.OrderId);
            if (order == null)
                return NotFound("Order not found");

            
            if (order.Status == OrderStatus.Shipped 
                || order.Status == OrderStatus.Delivered
                || order.Status == OrderStatus.Cancelled)
                return BadRequest("Order already shipped");
           
            var shipment = new Shipment
            {
                OrderId = shipmentDto.OrderId,
                CarrierName = shipmentDto.ShippingCompany,
                TrackingNumber = shipmentDto.TrackingNumber,
                Address = shipmentDto.Address,
                ShippedAt = DateTime.UtcNow,       
                Status = OrderStatus.Shipped,
            };
            ShipmentRepository.Add(shipment);
            order.Status = OrderStatus.Shipped;
            OrderRepository.Update(order);
            ShipmentRepository.save();
            OrderRepository.save();

            return Ok (shipment);

        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult UpdateShipment([FromBody] UpdateShipmentDto shipmentDto , long id)
        {
            var shipment = ShipmentRepository.GetShipment(id);
            if (shipment == null)
                return NotFound();
            var order = OrderRepository.GetById(shipment.OrderId);
            if (order == null)
                return NotFound("Order not found");
            shipment.Status = shipmentDto.Status;

            shipment.DeliveredAt = shipmentDto.DeliveredAt;
            if (shipmentDto.Status == OrderStatus.Delivered)
            {
                shipment.DeliveredAt = DateTime.UtcNow;
                order.Status = OrderStatus.Delivered;
            }

            
            if (shipmentDto.Status == OrderStatus.Cancelled)
            {
                order.Status = OrderStatus.Cancelled;
            }
            ShipmentRepository.Update(shipment);
            OrderRepository.Update(order);
            ShipmentRepository.save();
            return Ok (shipment);

        }
    }
}
