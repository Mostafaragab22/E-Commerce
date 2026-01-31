using E_Commerce.Models;

namespace E_Commerce.DTOs.UserDTO
{
    public class UserResponseDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public UserStatus Status { get; set; }
    }
}
