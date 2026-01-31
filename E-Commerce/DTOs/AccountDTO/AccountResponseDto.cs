namespace E_Commerce.DTOs.AccountDTO
{
    public class AccountResponseDto
    {
        public string Token { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
