using FoodService.Models.Entities;
using FoodServiceAPI.Core.Command;
using FoodServiceAPI.Core.Service.Interface;
using Moq;
using System.Diagnostics.CodeAnalysis;
using FoodServiceApi.Tests.TestHelper;

namespace FoodServiceApi.Tests.Core.Command
{
    [ExcludeFromCodeCoverage]
    public class ProductCommandTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductCommand _productCommand;

        public ProductCommandTests()
        {
            _mockProductService = new Mock<IProductService>();
            _productCommand = new ProductCommand(_mockProductService.Object);
        }

        [Fact(DisplayName = "GetAllProducts - Success - Returns list of products")]
        public async Task GetAllProducts_Success_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product> { ProductTestHelper.Product };
            _mockProductService.SetupGetAllProductService(products);

            // Act
            var result = await _productCommand.GetAllProducts();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(products, result.Data);
        }

        [Fact(DisplayName = "GetProductById - Success - Returns product")]
        public async Task GetProductById_Success_ReturnsProduct()
        {
            // Arrange
            var product = ProductTestHelper.Product;
            _mockProductService.SetupGetProductByIdService(product.Id, product);

            // Act
            var result = await _productCommand.GetProductById(product.Id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(product, result.Data);
        }

        [Fact(DisplayName = "GetProductById - Failure - Product not found")]
        public async Task GetProductById_Failure_ProductNotFound()
        {
            // Arrange
            var nonExistentProductId = 999;
            _mockProductService.SetupGetProductByIdService(nonExistentProductId, null);

            // Act
            var result = await _productCommand.GetProductById(nonExistentProductId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Equal("Product not found", result.Message);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact(DisplayName = "CreateProduct - Success - Returns created product")]
        public async Task CreateProduct_Success_ReturnsCreatedProduct()
        {
            // Arrange
            var createdProduct = ProductTestHelper.Product;
            _mockProductService.SetupCreateProductService(createdProduct);
            _mockProductService.SetupGetProductByIdService(createdProduct.Id, createdProduct);

            // Act
            var result = await _productCommand.CreateProduct(createdProduct);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(createdProduct.Id, result.Data.Id);
        }

        [Fact(DisplayName = "CreateProduct - Failure - Returns error response")]
        public async Task CreateProduct_Failure_ReturnsErrorResponse()
        {
            // Arrange
            var newProduct = new Product { Id = 2 };
            _mockProductService.SetupCreateProductService(newProduct);

            // Act
            var result = await _productCommand.CreateProduct(newProduct);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
        }

        [Fact(DisplayName = "UpdateProduct - Success - Returns updated product")]
        public async Task UpdateProduct_Success_ReturnsUpdatedProduct()
        {
            // Arrange
            var updatedProduct = ProductTestHelper.Product;
            updatedProduct.Brand = "newBrand";
            _mockProductService.SetupUpdateProductService(updatedProduct);

            // Act
            var result = await _productCommand.UpdateProduct(updatedProduct.Id, updatedProduct);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(updatedProduct.Id, result.Data?.Id);
            Assert.Equal(updatedProduct.Brand, result.Data!.Brand);

        }

        [Fact(DisplayName = "UpdateProduct - Failure - Product ID mismatch")]
        public async Task UpdateProduct_Failure_ProductIdMismatch()
        {
            // Arrange
            var productToUpdate = ProductTestHelper.Product;
            var incorrectProductId = productToUpdate.Id + 1;

            // Act
            var result = await _productCommand.UpdateProduct(incorrectProductId, productToUpdate);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The Product ID in the URL does not match the Product ID in the request body", result.Message);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact(DisplayName = "DeleteProduct - Success - Returns true")]
        public async Task DeleteProduct_Success_ReturnsTrue()
        {
            // Arrange
            var productId = ProductTestHelper.Product.Id;
            _mockProductService.SetupGetProductByIdService(productId, ProductTestHelper.Product); 
            _mockProductService.SetupDeleteProductService(productId, true);

            // Act
            var result = await _productCommand.DeleteProduct(productId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
        }


        [Fact(DisplayName = "DeleteProduct - Failure - Product not found")]
        public async Task DeleteProduct_Failure_ProductNotFound()
        {
            // Arrange
            var nonExistentProductId = 999;
            _mockProductService.SetupGetProductByIdService(nonExistentProductId, null);

            // Act
            var result = await _productCommand.DeleteProduct(nonExistentProductId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.False(result.Data);
            Assert.Equal("Product not found", result.Message);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
