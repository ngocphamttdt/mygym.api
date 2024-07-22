using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGym.Request;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            var data = (await _productService.GetProducts()).ToList();
            return data;
        }

        [HttpPost]
        public async Task Post([FromBody] ProductRequest product)
        {
            var data = new ProductDto
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                IsActive= true
            };
            await _productService.Create(data);
        }

        [HttpPut]
        public async Task Put([FromBody] ProductRequest product)
        {
            var productData = new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
            await _productService.Update(productData);
        }
        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
           await _productService.Delete(id);
        }
    }
}
