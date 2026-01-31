using E_Commerce.DTOs.BrandDTO;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        IBrandRepository BrandRepository { get; set; }
        public BrandController(IBrandRepository brandRepository)
        {
            BrandRepository = brandRepository;

        }

        [HttpGet]
       
        public ActionResult<List<BrandDto>> GetAllBrand()
        {
            var brand = BrandRepository.GetAll()
            .Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Logo = b.Logo,
                IsActive = b.IsActive,
                Products = b.Products.Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SalePrice = p.SalePrice,
                    IsActive = p.IsActive,
                    BrandName = b.Name

                }).ToList()
                 
            }).ToList();

            return Ok(brand);

        }

        [HttpGet("{id}")]
      
        public ActionResult <BrandDto> GetBrand(long id)
        {
          var b = BrandRepository.GetById(id);
            if (b == null)
            {
                return NotFound();
            }
            var brand = new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Logo = b.Logo,
                IsActive = b.IsActive,

            };

            return Ok(brand);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBrand(CreateBrandDto brandDto)
        {
            Brand brand = new Brand();
            {

                brand.Name = brandDto.Name;
                brand.Logo = brandDto.Logo;
                brand.Description = brandDto.Description;
            }
            BrandRepository.Add(brand);
            BrandRepository.save();
            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult updateBrand(long id, UpdateBrandDto brandDto)
        {
            var brand = BrandRepository.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }

            brand.Name = brandDto.Name;
            brand.Description = brandDto.Description;
            brand.Logo = brandDto.Logo;
           
            BrandRepository.Update(brand);
            BrandRepository.save();
            return Ok(brand);
 
        }
        

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBrand(long id)
        {
            var brand = BrandRepository.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            BrandRepository.Delete(id);
            BrandRepository.save();
            return Ok("Deleted");
        }
    }
}