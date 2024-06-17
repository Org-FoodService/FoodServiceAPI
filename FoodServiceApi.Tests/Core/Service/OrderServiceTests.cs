using FoodService.Models.Entities;
using FoodServiceAPI.Core.Service;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using FoodServiceApi.Tests.TestsBase;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FoodServiceApi.Tests.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockOrderRepository.Object);
        }

        [Fact(DisplayName = "CreateOrder - Success - Returns created order")]
        public async Task CreateOrder_Success_ReturnsCreatedOrder()
        {
            // Arrange
            var newOrder = OrderTestHelper.Order;
            _mockOrderRepository.SetupCreateOrderRepository(newOrder, newOrder);

            // Act
            var result = await _orderService.CreateOrder(newOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newOrder.OrderId, result.OrderId);
        }

        [Fact(DisplayName = "DeleteOrder - Success - Returns true")]
        public async Task DeleteOrder_Success_ReturnsTrue()
        {
            // Arrange
            var orderId = OrderTestHelper.Order.OrderId;
            _mockOrderRepository.SetupGetByIdOrderRepository(orderId, OrderTestHelper.Order);
            _mockOrderRepository.SetupDeleteOrderRepository(OrderTestHelper.Order, true);

            // Act
            var result = await _orderService.DeleteOrder(orderId);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "DeleteOrder - Failure - Order not found")]
        public async Task DeleteOrder_Failure_OrderNotFound()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _mockOrderRepository.SetupGetByIdOrderRepository(nonExistentOrderId, null);

            // Act
            var result = await _orderService.DeleteOrder(nonExistentOrderId);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "GetAllOrder - Success - Returns list of orders")]
        public async Task GetAllOrder_Success_ReturnsListOfOrders()
        {
            // Arrange
            var orders = new List<Order> { OrderTestHelper.Order };
            _mockOrderRepository.SetupListAllOrdersRepository(orders);

            // Act
            var result = await _orderService.GetAllOrder();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orders.Count, result.Count);
        }

        [Fact(DisplayName = "GetOrderById - Success - Returns order")]
        public async Task GetOrderById_Success_ReturnsOrder()
        {
            // Arrange
            var orderId = OrderTestHelper.Order.OrderId;
            _mockOrderRepository.SetupGetByIdOrderRepository(orderId, OrderTestHelper.Order);

            // Act
            var result = await _orderService.GetOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
        }

        [Fact(DisplayName = "GetOrderById - Failure - Order not found")]
        public async Task GetOrderById_Failure_OrderNotFound()
        {
            // Arrange
            var nonExistentOrderId = 999;
            _mockOrderRepository.SetupGetByIdOrderRepository(nonExistentOrderId, null);

            // Act
            var result = await _orderService.GetOrderById(nonExistentOrderId);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "UpdateOrder - Success - Returns updated order")]
        public async Task UpdateOrder_Success_ReturnsUpdatedOrder()
        {
            // Arrange
            var updatedOrder = OrderTestHelper.Order;
            updatedOrder.OrderItems = new List<OrderItem> { new OrderItem { Comment = "newComment" } };
            _mockOrderRepository.SetupGetByIdOrderRepository(updatedOrder.OrderId, OrderTestHelper.Order);
            _mockOrderRepository.SetupUpdateOrderRepository(updatedOrder, 1);

            // Act
            var result = await _orderService.UpdateOrder(updatedOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedOrder.OrderId, result.OrderId);
            Assert.Equal(updatedOrder.OrderItems.First().Comment, result.OrderItems.First().Comment);
        }

        [Fact(DisplayName = "UpdateOrder - Failure - Order not found")]
        public async Task UpdateOrder_Failure_OrderNotFound()
        {
            // Arrange
            var updatedOrder = OrderTestHelper.Order;
            _mockOrderRepository.SetupGetByIdOrderRepository(updatedOrder.OrderId, null);

            // Act
            var result = await _orderService.UpdateOrder(updatedOrder);

            // Assert
            Assert.Null(result);
        }
    }
}
