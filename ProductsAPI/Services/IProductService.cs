using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(int id, ProductUpdate productDto);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> DecrementStockAsync(int id, int quantity);
        Task<bool> AddToStockAsync(int id, int quantity);
    }

}
