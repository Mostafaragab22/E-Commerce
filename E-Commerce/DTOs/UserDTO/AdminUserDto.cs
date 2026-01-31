using E_Commerce.Models;

namespace E_Commerce.DTOs.UserDTO
{
    public class AdminUserDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Status { get; set; }
    }
}
