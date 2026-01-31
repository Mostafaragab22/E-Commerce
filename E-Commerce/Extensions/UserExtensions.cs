using System.Security.Claims;

namespace E_Commerce.Extensions
{
    public static class UserExtensions
    {
        public static string UserId(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
