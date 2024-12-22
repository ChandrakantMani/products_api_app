using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsAPI.Controllers;
using ProductsAPI.DTO;
using ProductsAPI.Models;
using ProductsAPI.Services;

namespace ProductsAPI.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productsController = new ProductsController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnOkWithProducts()
        {
            var products = new List<Product>
            {
                new Product {Name = "Testing",Description = "Testing",Price = 100,Stock = 10}
            };
            _productServiceMock.Setup(service => service.GetAllProductsAsync())
                .ReturnsAsync(products);

            var result = await _productsController.GetAllProducts();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnOk_WhenProductExists()
        {
            var product = new Product {Name = "iPhone", Description = "Phone Model", Price = 100, Stock = 10 };
            _productServiceMock.Setup(service => service.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            var result = await _productsController.GetProductById(1);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(product);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            _productServiceMock.Setup(service => service.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            var result = await _productsController.GetProductById(999);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnCreatedResult()
        {
            var productDto = new ProductCreate
            {
                Name = "Test",
                Description = "Test Description",
                Price = 100,
                Stock = 10
            };

            var createdProduct = new Product
            {
                Id = 800298,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now
            };

            _productServiceMock.Setup(service => service.CreateProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(createdProduct);

            var result = await _productsController.CreateProduct(productDto);

            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult!.StatusCode.Should().Be(201);
            createdResult.Value.Should().BeEquivalentTo(createdProduct);
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            var productUpdateDto = new ProductUpdate
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 100,
                Stock = 20
            };

            _productServiceMock.Setup(service => service.UpdateProductAsync(800298, productUpdateDto))
                .ReturnsAsync(true);
            var result = await _productsController.UpdateProduct(800298, productUpdateDto);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            var productUpdateDto = new ProductUpdate
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 150,
                Stock = 20
            };

            _productServiceMock.Setup(service => service.UpdateProductAsync(800298, productUpdateDto))
                .ReturnsAsync(false);

            var result = await _productsController.UpdateProduct(800298, productUpdateDto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNoContent_WhenDeletionIsSuccessful()
        {

            _productServiceMock.Setup(service => service.DeleteProductAsync(800298))
                .ReturnsAsync(true);

            var result = await _productsController.DeleteProduct(800298);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            _productServiceMock.Setup(service => service.DeleteProductAsync(800298))
                .ReturnsAsync(false);

            var result = await _productsController.DeleteProduct(800298);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
