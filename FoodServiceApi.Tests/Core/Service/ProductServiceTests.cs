using FoodService.Models.Entities;
using FoodServiceAPI.Core.Service;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Moq;
using FoodServiceApi.Tests.TestHelper;
using System.Diagnostics.CodeAnalysis;

namespace FoodServiceApi.Tests.Core.Service
{
    [ExcludeFromCodeCoverage]
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Fact(DisplayName = "CreateProduct - Success - Returns created product")]
        public async Task CreateProduct_Success_ReturnsCreatedProduct()
        {
            // Arrange
            var newProduct = ProductTestHelper.Product;
            _mockProductRepository.SetupCreateProductRepository(newProduct, newProduct);

            // Act
            var result = await _productService.CreateProductAsync(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newProduct.Id, result.Id);
        }

        [Fact(DisplayName = "DeleteProduct - Success - Returns true")]
        public async Task DeleteProduct_Success_ReturnsTrue()
        {
            // Arrange
            var productId = ProductTestHelper.Product.Id;
            _mockProductRepository.SetupGetByIdProductRepository(productId, ProductTestHelper.Product);
            _mockProductRepository.SetupDeleteProductRepository(ProductTestHelper.Product, true);

            // Act
            var result = await _productService.DeleteProductAsync(productId);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "DeleteProduct - Failure - Product not found")]
        public async Task DeleteProduct_Failure_ProductNotFound()
        {
            // Arrange
            var nonExistentId = 999;
            _mockProductRepository.SetupGetByIdProductRepository(nonExistentId, null);

            // Act
            var result = await _productService.DeleteProductAsync(nonExistentId);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "GetAllProduct - Success - Returns list of products")]
        public async Task GetAllProduct_Success_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product> { ProductTestHelper.Product };
            _mockProductRepository.SetupListAllProductsRepository(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(products.Count, result.Count);
        }

        [Fact(DisplayName = "GetProductById - Success - Returns product")]
        public async Task GetProductById_Success_ReturnsProduct()
        {
            // Arrange
            var productId = ProductTestHelper.Product.Id;
            _mockProductRepository.SetupGetByIdProductRepository(productId, ProductTestHelper.Product);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact(DisplayName = "GetProductById - Failure - Product not found")]
        public async Task GetProductById_Failure_ProductNotFound()
        {
            // Arrange
            var nonExistentId = 999;
            _mockProductRepository.SetupGetByIdProductRepository(nonExistentId, null);

            // Act
            var result = await _productService.GetProductByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "UpdateProduct - Success - Returns updated product")]
        public async Task UpdateProduct_Success_ReturnsUpdatedProduct()
        {
            // Arrange
            var updatedProduct = ProductTestHelper.Product;
            updatedProduct.Brand = "newBrand";
            _mockProductRepository.SetupGetByIdProductRepository(updatedProduct.Id, ProductTestHelper.Product);
            _mockProductRepository.SetupUpdateProductRepository(updatedProduct, 1);

            // Act
            var result = await _productService.UpdateProductAsync(updatedProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedProduct.Id, result.Id);
            Assert.Equal(updatedProduct.Brand, result.Brand);
        }

        [Fact(DisplayName = "UpdateProduct - Failure - Product not found")]
        public async Task UpdateProduct_Failure_ProductNotFound()
        {
            // Arrange
            var updatedProduct = ProductTestHelper.Product;
            _mockProductRepository.SetupGetByIdProductRepository(updatedProduct.Id, null);

            // Act
            var result = await _productService.UpdateProductAsync(updatedProduct);

            // Assert
            Assert.Null(result);
        }
    }
}
