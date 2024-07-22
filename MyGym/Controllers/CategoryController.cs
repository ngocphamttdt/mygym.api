using Microsoft.AspNetCore.Mvc;
using MyGym.Request;
using MyGym.Response;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResponse>> Get()
        {
            var categories = (await _categoryService.GetAll()).Select(x=> new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            });
            return categories;
        }

        [HttpPost]
        public async Task Post([FromBody] CategoryRequest category)
        {
            var data = new CategoryDto
            {
                Name = category.Name,
                IsActive = category.IsActive

            };
           await _categoryService.CreateCategory(data);

        }
       
    }
}
