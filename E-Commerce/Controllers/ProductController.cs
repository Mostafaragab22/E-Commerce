using E_Commerce.DTOs.ProductDTO;
using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace E_Commerce.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository ProductRepository;
        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<List<ProductListDto>> GetAll()
        {
            var products = ProductRepository.GetAllProduct()
                .Select(P => new ProductListDto
                {
                    Id = P.Id,
                    Name = P.Name,
                    SalePrice = P.SalePrice,
                    IsActive = P.IsActive,
                    CategoryName = P.Category.Name,
                    BrandName = P.Brand.Name,



                }).ToList();

            return Ok(products);;
        }

        [HttpGet("{id}")]

        public ActionResult <ProductListDto> GetProduct(long id)

        {
            var p = ProductRepository.GetById(id);
            if (p == null)
            {
                return NotFound();
            }
            var productDto = new ProductListDto

            {
                Id = p.Id,
                Name = p.Name,
                SalePrice = p.SalePrice,
                IsActive = p.IsActive,
                CategoryName = p.Category?.Name,
                BrandName = p.Brand?.Name,


            };
            return Ok(productDto);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct([FromBody] CreateProductDto productDto)
        {
            var product = new Models.Product();

            {

                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Slug = productDto.Slug;
                product.SalePrice = productDto.SalePrice;
                product.BrandId = productDto.BrandId;
                product.CategoryId = productDto.CategoryId;
                product.BasePrice = productDto.BasePrice;
                product.IsActive = productDto.IsActive;
                product.IsFeatured = productDto.IsFeatured;
                product.IsDigital = productDto.IsDigital;
                product.MainImage = productDto.MainImage;
                product.Barcode = productDto.Barcode;



            }
            ProductRepository.Add(product);
            ProductRepository.save();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromBody] UpdateProductDto productDto , long id)
        { 
            var product = ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Slug = productDto.Slug;
            product.SalePrice = productDto.SalePrice;
            product.BrandId = productDto.BrandId;
            product.CategoryId = productDto.CategoryId;
            product.BasePrice = productDto.BasePrice;
            product.IsActive = productDto.IsActive;
            product.IsFeatured = productDto.IsFeatured;
            product.IsDigital = productDto.IsDigital;
            product.MainImage = productDto.MainImage;
            product.Barcode = productDto.Barcode;

            ProductRepository.Update(product);
            ProductRepository.save();
            return Ok (product);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct( long id)
        {
         var product = ProductRepository.GetById(id);
            if (product == null )
            {

                return NotFound();
            }
            ProductRepository.Delete(id);
            ProductRepository.save();
            return NoContent();


        }
    }
}
