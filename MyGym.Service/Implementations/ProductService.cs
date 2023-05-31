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
            var data = await _productRepo.Get(x => x.IsActive)
                .Include(x => x.Category)
                .Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                Price = x.Price,
                IsActive = x.IsActive
            }).ToListAsync();

            //var products = _context.Products.ToList();
            //foreach (var product in products)
            //{
            //    var categoryName = product.Category.Name;
            //}
            //var data1 = await _context.Products.Include(x => x.Category)
            //    .Where(x=>x.IsActive == true)
            //    .Select(x => new ProductDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CategoryId = x.CategoryId,
            //    CategoryName = x.Category.Name,
            //    Price = x.Price,
            //    IsActive = x.IsActive
            //}).ToListAsync();

            return data;
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
            var productTobeUpdated = ( _productRepo.Find(x => x.Id == product.Id)).FirstOrDefault();
            if (productTobeUpdated != null)
            {
                productTobeUpdated.Name = product.Name;
                productTobeUpdated.Price = product.Price;
                productTobeUpdated.CategoryId = product.CategoryId;
                await _productRepo.UpdateAsync(productTobeUpdated);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int productId)
        {
            var productTobeDeleted = ( _productRepo.Find(x => x.Id == productId)).FirstOrDefault();
            if (productTobeDeleted != null)  productTobeDeleted.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}
