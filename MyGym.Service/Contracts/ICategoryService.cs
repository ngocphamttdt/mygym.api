using MyGym.Service.DTOs;

namespace MyGym.Service.Contracts
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryDto category);
        Task<IEnumerable<CategoryDto>> GetAll();
    }
}
