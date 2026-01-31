using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class User : IdentityUser<long>
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; }

        public UserType UserType { get; set; } = UserType.Customer;
        public UserStatus Status { get; set; } = UserStatus.Active;

        public DateTime? EmailVerifiedAt { get; set; }
        public DateTime? PhoneVerifiedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }


        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();
    }

    public enum UserType { Customer = 1, Admin = 2, Support = 3 }
    public enum UserStatus { Active = 1, Suspended = 2 }
}
