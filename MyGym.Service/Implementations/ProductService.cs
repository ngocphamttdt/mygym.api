using Microsoft.EntityFrameworkCore;
using MyGym.Database;
using MyGym.Database.DAL.Contracts;
using MyGym.Database.Entities;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly MyGymContext _context;

        public ProductService(IRepository<Product> productRepo, MyGymContext context)
        {
            _productRepo = productRepo;
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var data1 = await _context.Products.Include(x => x.Category).Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                IsActive = x.IsActive
            }).ToListAsync();
            //var data = (await _productRepo.GetAsync()).ToList().Select(x => new ProductDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Price = x.Price
            //});
            return data1;
        }

        public async Task Create(ProductDto product)
        {
            var productTobeCreated = new Product
            {
                Name = product.Name,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive,
                Price = product.Price,
                CreatedDate = DateTime.Now
            };
            await _productRepo.AddAsync(productTobeCreated);
            await _context.SaveChangesAsync();
        }
        public async Task Update(ProductDto product)
        {
            var productTobeUpdated = (await _productRepo.FindAsync(x => x.Id == product.Id)).FirstOrDefault();
            if(productTobeUpdated != null)
            {
                productTobeUpdated.Name = product.Name;
                productTobeUpdated.Price = product.Price;
                productTobeUpdated.CategoryId = product.CategoryId;
                await _productRepo.UpdateAsync(productTobeUpdated);
                await _context.SaveChangesAsync();
            }
           
        }
    }
}
