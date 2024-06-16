using FoodService.Models.Entities;
using FoodService.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using FoodServiceApi.Tests.TestsBase;
using FoodServiceAPI.Controllers;
using FoodServiceAPI.Core.Command.Interface;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodServiceApi.Tests.API.Controller
{
    public class OrderControllerTests
    {
        protected readonly OrderController _controller;
        protected readonly Mock<IOrderCommand> _mockOrderCommand;
        protected readonly Mock<ILogger<OrderController>> _mockLogger;

        public OrderControllerTests(): base()
        {
            // Controller
            _mockOrderCommand = new Mock<IOrderCommand>();
            _mockLogger = new Mock<ILogger<OrderController>>();
            _controller = new OrderController(_mockOrderCommand.Object, _mockLogger.Object);

        }

        [Fact(DisplayName = "GetAllOrders - Success - Returns Ok with list of orders")]
        public async Task GetAllOrders_Success_ReturnsOk()
        {
            // Arrange
            var orders = new List<Order> { OrderTestHelper.Order };
            var response = ResponseCommon<List<Order>>.Success(orders);
            _mockOrderCommand.SetupGetAllOrdersCommand(response);

            // Act
            var result = await _controller.GetAllOrders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(orders, okResult.Value);
        }

        [Fact(DisplayName = "GetAllOrders - Failure - Returns Status Code")]
        public async Task GetAllOrders_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<List<Order>>.Failure("Failed to fetch orders", 500);
            _mockOrderCommand.SetupGetAllOrdersCommand(response);

            // Act
            var result = await _controller.GetAllOrders();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Failed to fetch orders", statusCodeResult.Value);
        }

        [Fact(DisplayName = "GetOrderById - Success - Returns Ok with order")]
        public async Task GetOrderById_Success_ReturnsOk()
        {
            // Arrange
            var order = OrderTestHelper.Order;
            var response = ResponseCommon<Order>.Success(order);
           _mockOrderCommand.SetupGetOrderByIdCommand(order.OrderId, response);

            // Act
            var result = await _controller.GetOrderById(order.OrderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(order, okResult.Value);
        }

        [Fact(DisplayName = "GetOrderById - Failure - Returns Status Code")]
        public async Task GetOrderById_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<Order>.Failure("Order not found", 404);
           _mockOrderCommand.SetupGetOrderByIdCommand(1, response);

            // Act
            var result = await _controller.GetOrderById(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, statusCodeResult.StatusCode);
            Assert.Equal("Order not found", statusCodeResult.Value);
        }

        [Fact(DisplayName = "CreateOrder - Success - Returns CreatedAtAction with order")]
        public async Task CreateOrder_Success_ReturnsCreatedAtAction()
        {
            // Arrange
            var order = OrderTestHelper.Order;
            var response = ResponseCommon<Order>.Success(order);
           _mockOrderCommand.SetupCreateOrderCommand(order, response);

            // Act
            var result = await _controller.CreateOrder(order);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(order, createdAtActionResult.Value);
            Assert.Equal(nameof(_controller.GetOrderById), createdAtActionResult.ActionName);
            Assert.Equal(order.OrderId, createdAtActionResult.RouteValues!["id"]);
        }

        [Fact(DisplayName = "CreateOrder - Failure - Returns Status Code")]
        public async Task CreateOrder_Failure_ReturnsStatusCode()
        {
            // Arrange
            var order = OrderTestHelper.Order;
            var response = ResponseCommon<Order>.Failure("Failed to create order", 400);
           _mockOrderCommand.SetupCreateOrderCommand(order, response);

            // Act
            var result = await _controller.CreateOrder(order);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to create order", statusCodeResult.Value);
        }

        [Fact(DisplayName = "UpdateOrder - Success - Returns Ok with updated order")]
        public async Task UpdateOrder_Success_ReturnsOk()
        {
            // Arrange
            var order = OrderTestHelper.Order;
            var response = ResponseCommon<Order>.Success(order);
           _mockOrderCommand.SetupUpdateOrderCommand(order.OrderId, order, response!);

            // Act
            var result = await _controller.UpdateOrder(order.OrderId, order);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(order, okResult.Value);
        }

        [Fact(DisplayName = "UpdateOrder - Failure - Returns Status Code")]
        public async Task UpdateOrder_Failure_ReturnsStatusCode()
        {
            // Arrange
            var order = OrderTestHelper.Order;
            var response = ResponseCommon<Order>.Failure("Failed to update order", 400);
           _mockOrderCommand.SetupUpdateOrderCommand(order.OrderId, order, response!);

            // Act
            var result = await _controller.UpdateOrder(order.OrderId, order);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to update order", statusCodeResult.Value);
        }

        [Fact(DisplayName = "DeleteOrder - Success - Returns NoContent")]
        public async Task DeleteOrder_Success_ReturnsNoContent()
        {
            // Arrange
            var response = ResponseCommon<bool>.Success(true);
           _mockOrderCommand.SetupDeleteOrderCommand(1, response);

            // Act
            var result = await _controller.DeleteOrder(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact(DisplayName = "DeleteOrder - Failure - Returns Status Code")]
        public async Task DeleteOrder_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<bool>.Failure("Failed to delete order", 400);
           _mockOrderCommand.SetupDeleteOrderCommand(1, response);

            // Act
            var result = await _controller.DeleteOrder(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to delete order", statusCodeResult.Value);
        }
    }
}
