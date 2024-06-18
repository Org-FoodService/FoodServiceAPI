using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Controllers;
using FoodServiceAPI.Core.Command.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using FoodServiceApi.Tests.TestHelper;

namespace FoodServiceApi.Tests.API.Controller
{
    [ExcludeFromCodeCoverage]
    public class ProductControllerTests
    {
        protected readonly ProductController _controller;
        protected readonly Mock<IProductCommand> _mockProductCommand;
        protected readonly Mock<ILogger<ProductController>> _mockLogger;

        public ProductControllerTests() : base()
        {
            // Controller
            _mockProductCommand = new Mock<IProductCommand>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_mockProductCommand.Object, _mockLogger.Object);

        }

        [Fact(DisplayName = "GetAllProducts - Success - Returns Ok with list of products")]
        public async Task GetAllProducts_Success_ReturnsOk()
        {
            // Arrange
            var products = new List<Product> { ProductTestHelper.Product };
            var response = ResponseCommon<List<Product>>.Success(products);
            _mockProductCommand.SetupGetAllProductsCommand(response);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseCommon<List<Product>>>(okResult.Value);
            Assert.Equal(products, returnValue.Data);
        }

        [Fact(DisplayName = "GetAllProducts - Failure - Returns Status Code")]
        public async Task GetAllProducts_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<List<Product>>.Failure("Failed to fetch products", 500);
            _mockProductCommand.SetupGetAllProductsCommand(response);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            var returnValue = Assert.IsType<ResponseCommon<List<Product>>>(statusCodeResult.Value);
            Assert.Equal("Failed to fetch products", returnValue.Message);
        }

        [Fact(DisplayName = "GetProductById - Success - Returns Ok with product")]
        public async Task GetProductById_Success_ReturnsOk()
        {
            // Arrange
            var product = ProductTestHelper.Product;
            var response = ResponseCommon<Product>.Success(product);
            _mockProductCommand.SetupGetProductByIdCommand(product.Id, response);

            // Act
            var result = await _controller.GetProductById(product.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact(DisplayName = "GetProductById - Failure - Returns Status Code")]
        public async Task GetProductById_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<Product>.Failure("Product not found", 404);
            _mockProductCommand.SetupGetProductByIdCommand(1, response);

            // Act
            var result = await _controller.GetProductById(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
            var returnValue = Assert.IsType<ResponseCommon<Product>>(statusCodeResult.Value);
            Assert.Equal("Product not found", returnValue.Message);
        }

        [Fact(DisplayName = "CreateProduct - Success - Returns CreatedAtAction with product")]
        public async Task CreateProduct_Success_ReturnsCreatedAtAction()
        {
            // Arrange
            var product = ProductTestHelper.Product;
            var response = ResponseCommon<Product>.Success(product);
            _mockProductCommand.SetupCreateProductCommand(product, response);

            // Act
            var result = await _controller.CreateProduct(product);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(response, createdAtActionResult.Value);
            Assert.Equal(nameof(_controller.GetProductById), createdAtActionResult.ActionName);
            Assert.Equal(product.Id, createdAtActionResult.RouteValues!["id"]);
        }

        [Fact(DisplayName = "CreateProduct - Failure - Returns Status Code")]
        public async Task CreateProduct_Failure_ReturnsStatusCode()
        {
            // Arrange
            var product = ProductTestHelper.Product;
            var response = ResponseCommon<Product>.Failure("Failed to create product", 400);
            _mockProductCommand.SetupCreateProductCommand(product, response);

            // Act
            var result = await _controller.CreateProduct(product);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            var returnValue = Assert.IsType<ResponseCommon<Product>>(statusCodeResult.Value);
            Assert.Equal("Failed to create product", returnValue.Message);
        }

        [Fact(DisplayName = "UpdateProduct - Success - Returns Ok with updated product")]
        public async Task UpdateProduct_Success_ReturnsOk()
        {
            // Arrange
            var product = ProductTestHelper.Product;
            var response = ResponseCommon<Product>.Success(product);
            _mockProductCommand.SetupUpdateProductCommand(product.Id, product, response!);

            // Act
            var result = await _controller.UpdateProduct(product.Id, product);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact(DisplayName = "UpdateProduct - Failure - Returns Status Code")]
        public async Task UpdateProduct_Failure_ReturnsStatusCode()
        {
            // Arrange
            var product = ProductTestHelper.Product;
            var response = ResponseCommon<Product>.Failure("Failed to update product", 400);
            _mockProductCommand.SetupUpdateProductCommand(product.Id, product, response!);

            // Act
            var result = await _controller.UpdateProduct(product.Id, product);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            var returnValue = Assert.IsType<ResponseCommon<Product>>(statusCodeResult.Value);
            Assert.Equal("Failed to update product", returnValue.Message);
        }

        [Fact(DisplayName = "DeleteProduct - Success - Returns NoContent")]
        public async Task DeleteProduct_Success_ReturnsNoContent()
        {
            // Arrange
            var response = ResponseCommon<bool>.Success(true);
            _mockProductCommand.SetupDeleteProductCommand(1, response);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact(DisplayName = "DeleteProduct - Failure - Returns Status Code")]
        public async Task DeleteProduct_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<bool>.Failure("Failed to delete product", 400);
            _mockProductCommand.SetupDeleteProductCommand(1, response);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            var returnValue = Assert.IsType<ResponseCommon<bool>>(statusCodeResult.Value);
            Assert.Equal("Failed to delete product", returnValue.Message);
        }
    }
}
