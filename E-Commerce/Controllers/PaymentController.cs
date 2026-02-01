using E_Commerce.DTOs.PaymentDTO;
using E_Commerce.DTOs.Payments;
using E_Commerce.Extensions;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    IPaymentRepository PaymentRepository { get; set; }
    IOrderRepository orderRepository { get; set; }

    public PaymentController(IPaymentRepository paymentRepository , IOrderRepository _orderRepository) 
    {
        PaymentRepository = paymentRepository;
        orderRepository = _orderRepository;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetMyPayment()
    {
        var userIdString = User.UserId();

        if (!long.TryParse(userIdString, out var userId))
            return BadRequest("Invalid User");

       
        var payments = PaymentRepository.GetByUserId(userId);

        var paymentDtos = payments.Select(payment => new PaymentResponseDto
        {
            Id = payment.Id,
            PaymentMethod = payment.PaymentMethod,
            Amount = payment.Amount,
            Status = payment.Status,
            TransactionReference = payment.TransactionReference
        }).ToList();

        return Ok(paymentDtos);
    }

   
    [HttpPost]
    [Authorize]
    public IActionResult CreatePayment(CreatePaymentDto paymentDto)
    {
        var userIdString = User.UserId();
        if (!long.TryParse(userIdString, out var userId))
            return BadRequest("Invalid User");
        var order = orderRepository.GetById(paymentDto.OrderId);
        if (order == null)
            return NotFound("Order not found");

        
        if (order.UserId != userId)
            return Forbid("You are not allowed to pay for this order");

        
        if (order.PaymentStatus == PaymentStatus.Paid)
            return BadRequest("Order is already paid");


        var payment = new Payment
        {
            UserId = userId,                
            OrderId = paymentDto.OrderId,    
            PaymentMethod = paymentDto.PaymentMethod,
            Amount = paymentDto.Amount,
            Currency = "EGP",
            GatewayResponse = "Success",
            Status = PaymentStatus.Pending,  
            TransactionReference = Guid.NewGuid().ToString() 
        };

       
        PaymentRepository.Add(payment);
        order.PaymentStatus = PaymentStatus.Pending;
        orderRepository.Update(order);
        PaymentRepository.save();

        return CreatedAtAction(nameof(GetMyPayment), new { id = payment.Id }, paymentDto);

    }
}
