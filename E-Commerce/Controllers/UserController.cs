using E_Commerce.DTOs.UserDTO;
using E_Commerce.Extensions;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository UserRepository { get; set; }

        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpGet("MyProfile")]
        [Authorize]
        public IActionResult GetMyProfile()
        {
            var UserIdString = User.UserId();
            if (!long.TryParse(UserIdString, out var userId))
                return BadRequest("Invalid User");
            var user = UserRepository.GetById(userId);
            if (user == null)
                return NotFound();
           
            var userDto = new UserResponseDto
            {
                Id = userId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Status = user.Status,
                UserType = user.UserType,

            };
            return Ok(userDto);

        }
        [HttpPut("UpdateMyProfile")]
        [Authorize]
        public IActionResult UpdateMyProfile(UpdateUserProfileDto userDto)
        {
            var UserIdString = User.UserId();
            if (!long.TryParse(UserIdString, out var userId))
                return BadRequest("Invaild User ");
            var user = UserRepository.GetById(userId);
            if (user == null)
                return NotFound();
            
            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.Phone;

           
            UserRepository.Update(user);
            UserRepository.save();
            return Ok(user);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<UserResponseDto>> GetAllUser()
        {
            var user = UserRepository.GetAll()
                 .Select(x => new UserResponseDto
                 {
                     Id = x.Id,
                     FullName = x.FullName,
                     Phone = x.PhoneNumber,
                     Status = x.Status,
                     UserType = x.UserType,
                 }).ToList();
            return Ok(user);

        }

        [HttpPut("{id}/type")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUserType(long id ,AdminUserDto userDto)
        {
            
            var user = UserRepository.GetById(id);
            if (user == null)
                return NotFound();


            if (user.UserType == userDto.UserType)
                return BadRequest("User already has this type");

            user.UserType = userDto.UserType;


            UserRepository.Update(user);
            UserRepository.save();
            return Ok(user);

        }
                 

    }
}
