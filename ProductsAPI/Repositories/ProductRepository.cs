using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
            => await _dbContext.Products.ToListAsync();

        public async Task<Product?> GetProductByIdAsync(int id)
            => await _dbContext.Products.FindAsync(id);

        public async Task<Product> AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ProductExistsAsync(int id)
            => await _dbContext.Products.AnyAsync(p => p.Id == id);

        public async Task<int> GenerateUniqueRandomIdAsync()
        {
            var random = new Random();
            while (true)
            {
                int id = random.Next(100000, 999999);

                if (!await _dbContext.Products.AnyAsync(p => p.Id == id))
                {
                    return id;
                }
            }
        }
    }
}
