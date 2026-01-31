using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.AccountDTO
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

    }
}
