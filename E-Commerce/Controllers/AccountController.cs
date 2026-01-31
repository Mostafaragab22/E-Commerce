using E_Commerce.DTOs.AccountDTO;
using E_Commerce.Extensions;
using E_Commerce.Models;
using E_Commerce.Repository;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Common;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IUserRepository UserRepository { get; set; }
        private readonly UserManager<User> userManager;
        private readonly IConfiguration config;

       
        public AccountController(IUserRepository userRepository,IConfiguration _config, UserManager<User> UserManager)
        {
            UserRepository = userRepository;
            config = _config;
            UserRepository = userRepository;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerdto)
        {
            if(ModelState.IsValid)
            {
                var account = new User
                {
                    FullName = registerdto.Email,
                    PhoneNumber = registerdto.Phone,
                    Email = registerdto.Email,
                };
                IdentityResult result = await userManager.CreateAsync(account, registerdto.Password);
                if (result.Succeeded)
                {
                    return Ok("created");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);

                }
            }
            return BadRequest(ModelState);

        }

       
        [HttpPost("Login")]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult> Login(LoginDTO UserFromRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userFromDb = await userManager.FindByEmailAsync(UserFromRequest.Email);
            if (userFromDb == null)
                return BadRequest("Invalid email or password");

            if (!await userManager.CheckPasswordAsync(userFromDb, UserFromRequest.Password))
                return BadRequest("Invalid email or password");

            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromDb.FullName),
            };
            var userRole = await userManager.GetRolesAsync(userFromDb);
            foreach (var RoleName in userRole)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, RoleName));
            }
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecuretyKey"]));
            SigningCredentials signingCred = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken MyToken = new JwtSecurityToken
                (
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: userClaims,
                signingCredentials: signingCred,
                expires: DateTime.UtcNow.AddMinutes(
                int.Parse(config["JWT:TokenLifetimeMinutes"]))
                
                );

            return Ok(new

            {

                token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                expiration = MyToken.ValidTo

            });
                       
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
           
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userId))
                return Unauthorized();

            var user = await userManager.FindByIdAsync(userIdString);
            if (user == null)
                return Unauthorized();

            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest("New password and confirm password do not match");

            var result = await userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword
            );

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return BadRequest(ModelState);
            }

            return Ok("Password changed successfully");
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Ok(); 

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"https://frontend/reset-password?email={dto.Email}&token={Uri.EscapeDataString(token)}";

            return Ok("Reset password link sent to email");
        }

        [HttpPost("reset-password")]
        [EnableRateLimiting("login")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("Invalid request");

            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest("Passwords do not match");


            var result = await userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword
            );

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return BadRequest(ModelState);
            }

            return Ok("Password reset successfully");
        }



    }
}

         