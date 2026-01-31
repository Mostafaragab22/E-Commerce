using E_Commerce.DTOs.AddressDTO;
using E_Commerce.DTOs.Addresses;
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
    public class AddressController : ControllerBase
    {
        IAddressRepository AddressRepository { get; set; }
        
        public AddressController(IAddressRepository addressRepository)
        {
            AddressRepository = addressRepository;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetMyAddress(long id)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userid))
                return BadRequest("Invaild User");
            var address = AddressRepository.GetById(id);
            if (address == null) 
                return NotFound();
            if (address.UserId != userid)
                return Forbid();
            var address1 = new AddressDto()
            {
                Id = address.Id,
                FullName = address.FullName,
                Phone = address.Phone,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                IsDefaultShipping = address.IsDefaultShipping,
                IsDefaultBilling = address.IsDefaultBilling,

            };
            return Ok(address1);           
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateAdress(CreateAddressDto addressDto)
        {
            var userIdString = User.UserId();
            if (!long.TryParse(userIdString, out var userid))
                return BadRequest("Invalid User");
            var address = new Address();
            {
                address.UserId = userid;
                address.FullName = addressDto.FullName;
                address.Phone = addressDto.Phone;
                address.Country = addressDto.Country;
                address.City = addressDto.City;
                address.Area = addressDto.Area;
                address.Street = addressDto.Street;
                address.Building = addressDto.Building;
                address.Apartment = addressDto.Apartment;
                address.PostalCode = addressDto.PostalCode;
                address.IsDefaultBilling = addressDto.IsDefaultBilling;
                address.IsDefaultShipping = addressDto.IsDefaultShipping;
            }
            AddressRepository.Add(address);
            AddressRepository.save();
            return CreatedAtAction(nameof(GetMyAddress),new {id=address.Id} ,address);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateAddress( long id ,UpdateAddressDto addressDto)
        {
            var UserIdString = User.UserId();
            if (!long.TryParse(UserIdString, out var userid))
                return BadRequest("Invalid User");
            var address = AddressRepository.GetById(id);
            if (address == null)
                return NotFound();
            if (address.UserId != userid)
                return Forbid();
            
            
            address.FullName = addressDto.FullName;
            address.Phone = addressDto.Phone;
            address.Country = addressDto.Country;
            address.City = addressDto.City;
            address.Area = addressDto.Area;
            address.Street = addressDto.Street;
            address.Building = addressDto.Building;
            address.Apartment = addressDto.Apartment;
            address.PostalCode = addressDto.PostalCode;
            address.IsDefaultBilling = addressDto.IsDefaultBilling;
            address.IsDefaultShipping = addressDto.IsDefaultShipping;

            AddressRepository.Update(address);
            AddressRepository.save();
            return Ok(addressDto);


        }    

    }
}
