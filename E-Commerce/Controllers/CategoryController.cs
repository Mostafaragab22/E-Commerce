using E_Commerce.DTOs.Categories;
using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    { 
        ICategoryRepository CategoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;

        }
       
        [HttpGet]
        public ActionResult<List<CategoryDto>> GetAllCategory()
        {
            var category = CategoryRepository.GetAll()
            .Select(C => new CategoryDto
            {
                Id = C.Id,
                Name = C.Name,
                Description = C.Description,
                IsActive = C.IsActive,
                Products = C.Products.Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SalePrice = p.SalePrice,
                    IsActive = p.IsActive,
                    CategoryName = C.Name,
                    BrandName = p.Brand.Name

                }).ToList() ?? new List<ProductListDto>()
            }).ToList();
            return Ok(category);

        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var C = CategoryRepository.GetById(id);
            {
                if (C == null)
                {
                    return NotFound();
                }
                var category = new CategoryDto
                {

                    Id = C.Id,
                    Name = C.Name,
                    Description = C.Description,
                    IsActive = C.IsActive,
                };
                return Ok(category);

            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory([FromBody] CreateCategoryDto categoryDto)
        {
            var category = new Category();
            {
                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.Image = categoryDto.Image;
                category.Slug = categoryDto.Slug;
            }
            
            CategoryRepository.Add(category);
            CategoryRepository.save();
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(long id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var category = CategoryRepository.GetById(id);
            if (category == null)

                {
                    return NotFound();
                }

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.Image = categoryDto.Image;

            CategoryRepository.Update(category);
            CategoryRepository.save();
            return Ok(category);
            }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryRepository.Delete(id);
            CategoryRepository.save();
            return NoContent();


        }
    }
}
