using FoodService.Models.Entities;
using FoodServiceAPI.Core.Service;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Moq;
using FoodServiceApi.Tests.TestHelper;
using System.Diagnostics.CodeAnalysis;
using FoodService.Models.Auth.User;

namespace FoodServiceApi.Tests.Core.Service
{
    [ExcludeFromCodeCoverage]
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
            var orderDto = OrderTestHelper.OrderDto;
            _mockOrderRepository.SetupCreateOrderRepository(newOrder, newOrder);

            // Act
            var result = await _orderService.CreateOrder(orderDto, new() { CpfCnpj = ""});

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newOrder.Id, result.Id);
        }

        [Fact(DisplayName = "DeleteOrder - Success - Returns true")]
        public async Task DeleteOrder_Success_ReturnsTrue()
        {
            // Arrange
            var Id = OrderTestHelper.Order.Id;
            _mockOrderRepository.SetupGetByIdOrderRepository(Id, OrderTestHelper.Order);
            _mockOrderRepository.SetupDeleteOrderRepository(OrderTestHelper.Order, true);

            // Act
            var result = await _orderService.DeleteOrder(Id);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "DeleteOrder - Failure - Order not found")]
        public async Task DeleteOrder_Failure_OrderNotFound()
        {
            // Arrange
            var nonExistentId = 999;
            _mockOrderRepository.SetupGetByIdOrderRepository(nonExistentId, null);

            // Act
            var result = await _orderService.DeleteOrder(nonExistentId);

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
            var Id = OrderTestHelper.Order.Id;
            _mockOrderRepository.SetupGetByIdOrderRepository(Id, OrderTestHelper.Order);

            // Act
            var result = await _orderService.GetOrderById(Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Id, result.Id);
        }

        [Fact(DisplayName = "GetOrderById - Failure - Order not found")]
        public async Task GetOrderById_Failure_OrderNotFound()
        {
            // Arrange
            var nonExistentId = 999;
            _mockOrderRepository.SetupGetByIdOrderRepository(nonExistentId, null);

            // Act
            var result = await _orderService.GetOrderById(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "UpdateOrder - Success - Returns updated order")]
        public async Task UpdateOrder_Success_ReturnsUpdatedOrder()
        {
            // Arrange
            var updatedOrder = OrderTestHelper.Order;
            updatedOrder.OrderItems = new List<OrderItem> { new OrderItem { Comment = "newComment" } };
            _mockOrderRepository.SetupGetByIdOrderRepository(updatedOrder.Id, OrderTestHelper.Order);
            _mockOrderRepository.SetupUpdateOrderRepository(updatedOrder, 1);

            // Act
            var result = await _orderService.UpdateOrder(updatedOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedOrder.Id, result.Id);
            Assert.Equal(updatedOrder.OrderItems.First().Comment, result.OrderItems.First().Comment);
        }

        [Fact(DisplayName = "UpdateOrder - Failure - Order not found")]
        public async Task UpdateOrder_Failure_OrderNotFound()
        {
            // Arrange
            var updatedOrder = OrderTestHelper.Order;
            _mockOrderRepository.SetupGetByIdOrderRepository(updatedOrder.Id, null);

            // Act
            var result = await _orderService.UpdateOrder(updatedOrder);

            // Assert
            Assert.Null(result);
        }
    }
}
