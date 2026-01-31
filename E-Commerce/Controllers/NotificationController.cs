using E_Commerce.DTOs.NotificationDTO;
using E_Commerce.Extensions;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        INotificationRepository NotificationRepository { get; set; }

        public NotificationsController(INotificationRepository notificationRepository)
        {
            NotificationRepository = notificationRepository;
        }

       
        [HttpGet]
        public IActionResult GetMyNotifications()
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid User");

            var notifications = NotificationRepository
                .GetByUserId(userId)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new GetMyNotification
                {
                   Id = n.Id,
                    Title= n.Title,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                })
                .ToList();

            return Ok(notifications);
        }

        [HttpPut("{id}/read")]
        public IActionResult MarkAsRead(long id)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return BadRequest("Invalid User");

            var notification = NotificationRepository.GetById(id);
            if (notification == null)
                return NotFound("Notification not found");

            if (notification.UserId != userId)
                return Forbid();

            if (notification.IsRead)
                return BadRequest("Notification already marked as read");

            notification.IsRead = true;
            NotificationRepository.Update(notification);
            NotificationRepository.save();

            return Ok("Notification marked as read");
        }
    }
}
