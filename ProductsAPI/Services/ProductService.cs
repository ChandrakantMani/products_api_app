using ProductsAPI.DTO;
using ProductsAPI.Models;
using ProductsAPI.Repositories;

namespace ProductsAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
            => await _productRepository.GetAllProductsAsync();

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.Id = await _productRepository.GenerateUniqueRandomIdAsync();
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<bool> UpdateProductAsync(int id, ProductUpdate productDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return false;

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;

            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return false;

            return await _productRepository.DeleteProductAsync(product);
        }

        public async Task<bool> DecrementStockAsync(int id, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null || product.Stock < quantity) return false;

            product.Stock -= quantity;
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> AddToStockAsync(int id, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return false;

            product.Stock += quantity;
            return await _productRepository.UpdateProductAsync(product);
        }
    }
}
