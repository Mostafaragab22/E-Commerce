using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.AccountDTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
