using MyGym.Database;
using MyGym.Database.DAL.Contracts;
using MyGym.Database.Entities;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepo;
        private readonly MyGymContext _context;
        public CategoryService(IRepository<Category> categoryRepo, MyGymContext context) {
            _categoryRepo = categoryRepo;
            _context = context;
        }
        public async Task CreateCategory(CategoryDto category)
        {
            var data = new Category
            {
                Name = category.Name,
                IsActive = category.IsActive
            };
            await _categoryRepo.AddAsync(data);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
           var data =  ( _categoryRepo.Get()).Select(x=> new CategoryDto
           {
               Id = x.Id,
               Name = x.Name,
                IsActive = x.IsActive
           }).ToList();
            return data;
        }
    }
}
