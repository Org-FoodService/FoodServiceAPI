using FoodService.Models.Entities;
using FoodServiceApi.Tests.TestHelper;
using FoodServiceAPI.Core.Command;
using FoodServiceAPI.Core.Service.Interface;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace FoodServiceApi.Tests.Core.Command
{
    [ExcludeFromCodeCoverage]
    public class OrderCommandTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderCommand _orderCommand;

        public OrderCommandTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _orderCommand = new OrderCommand(_mockOrderService.Object);
        }

        [Fact(DisplayName = "GetAllOrders - Success - Returns list of orders")]
        public async Task GetAllOrders_Success_ReturnsListOfOrders()
        {
            // Arrange
            var orders = new List<Order> { OrderTestHelper.Order };
            _mockOrderService.SetupGetAllOrderService(orders);

            // Act
            var result = await _orderCommand.GetAllOrders();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(orders, result.Data);
        }

        [Fact(DisplayName = "GetOrderById - Success - Returns order")]
        public async Task GetOrderById_Success_ReturnsOrder()
        {
            // Arrange
            var order = OrderTestHelper.Order;
            _mockOrderService.SetupGetOrderByIdService(order.OrderId, order);

            // Act
            var result = await _orderCommand.GetOrderById(order.OrderId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(order, result.Data);
        }

        [Fact(DisplayName = "GetOrderById - Failure - Order not found")]
        public async Task GetOrderById_Failure_OrderNotFound()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _mockOrderService.SetupGetOrderByIdService(nonExistentOrderId, null);

            // Act
            var result = await _orderCommand.GetOrderById(nonExistentOrderId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Equal("Order not found", result.Message);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact(DisplayName = "CreateOrder - Success - Returns created order")]
        public async Task CreateOrder_Success_ReturnsCreatedOrder()
        {
            // Arrange
            var createdOrder = OrderTestHelper.Order;
            _mockOrderService.SetupCreateOrderService(createdOrder);
            _mockOrderService.SetupGetOrderByIdService(createdOrder.OrderId, createdOrder);

            // Act
            var result = await _orderCommand.CreateOrder(createdOrder);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(createdOrder.OrderId, result.Data.OrderId);
        }

        [Fact(DisplayName = "CreateOrder - Failure - Returns error response")]
        public async Task CreateOrder_Failure_ReturnsErrorResponse()
        {
            // Arrange
            var newOrder = new Order { OrderId = 2 };
            _mockOrderService.SetupCreateOrderService(newOrder);

            // Act
            var result = await _orderCommand.CreateOrder(newOrder);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
        }

        [Fact(DisplayName = "UpdateOrder - Success - Returns updated order")]
        public async Task UpdateOrder_Success_ReturnsUpdatedOrder()
        {
            // Arrange
            var updatedOrder = OrderTestHelper.Order;
            updatedOrder.OrderItems = new() { new() { Comment = "newComment" } };
            _mockOrderService.SetupUpdateOrderService(updatedOrder);

            // Act
            var result = await _orderCommand.UpdateOrder(updatedOrder.OrderId, updatedOrder);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(updatedOrder.OrderId, result.Data?.OrderId);
            Assert.Equal(updatedOrder.OrderItems.First().Comment, result.Data!.OrderItems.First().Comment);

        }

        [Fact(DisplayName = "UpdateOrder - Failure - Order ID mismatch")]
        public async Task UpdateOrder_Failure_OrderIdMismatch()
        {
            // Arrange
            var orderToUpdate = OrderTestHelper.Order;
            var incorrectOrderId = orderToUpdate.OrderId + 1;

            // Act
            var result = await _orderCommand.UpdateOrder(incorrectOrderId, orderToUpdate);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The Order ID in the URL does not match the Order ID in the request body", result.Message);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact(DisplayName = "DeleteOrder - Success - Returns true")]
        public async Task DeleteOrder_Success_ReturnsTrue()
        {
            // Arrange
            var orderId = OrderTestHelper.Order.OrderId;
            _mockOrderService.SetupGetOrderByIdService(orderId, OrderTestHelper.Order); // Ensure order exists
            _mockOrderService.SetupDeleteOrderService(orderId, true);

            // Act
            var result = await _orderCommand.DeleteOrder(orderId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
        }


        [Fact(DisplayName = "DeleteOrder - Failure - Order not found")]
        public async Task DeleteOrder_Failure_OrderNotFound()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _mockOrderService.SetupGetOrderByIdService(nonExistentOrderId, null);

            // Act
            var result = await _orderCommand.DeleteOrder(nonExistentOrderId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.False(result.Data);
            Assert.Equal("Order not found", result.Message);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
