using MyGym.Service.DTOs;

namespace MyGym.Service.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task Create(ProductDto product);
        Task Update(ProductDto product);
        Task Delete(int productId);
    }
}
