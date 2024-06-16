using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Core.Command.Interface;
using FoodServiceAPI.Core.Service.Interface;
using Moq;

namespace FoodServiceApi.Tests.TestsBase
{
    public static class OrderTestHelper
    {
        public static readonly Order Order = new Order
        {
            OrderId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = 2,
                    ProductId = 1,
                    Quantity = 2,
                    Order = new Order
                    {
                        OrderId = 1
                    },
                    Comment = "Extra cheese",
                    OrderId = 1,
                    Product = new Product
                    {
                        Id = 1,
                        Name = "Pizza"
                    }
                }
            }
        };

        #region Setup Methods Command

        public static void SetupGetAllOrdersCommand(this Mock<IOrderCommand> mockOrderCommand, ResponseCommon<List<Order>> response)
        {
            mockOrderCommand.Setup(x => x.GetAllOrders()).ReturnsAsync(response);
        }

        public static void SetupGetOrderByIdCommand(this Mock<IOrderCommand> mockOrderCommand, int id, ResponseCommon<Order> response)
        {
            mockOrderCommand.Setup(x => x.GetOrderById(id)).ReturnsAsync(response);
        }

        public static void SetupCreateOrderCommand(this Mock<IOrderCommand> mockOrderCommand, Order order, ResponseCommon<Order> response)
        {
            mockOrderCommand.Setup(x => x.CreateOrder(order)).ReturnsAsync(response);
        }

        public static void SetupUpdateOrderCommand(this Mock<IOrderCommand> mockOrderCommand, int id, Order order, ResponseCommon<Order?> response)
        {
            mockOrderCommand.Setup(x => x.UpdateOrder(id, order)).ReturnsAsync(response);
        }

        public static void SetupDeleteOrderCommand(this Mock<IOrderCommand> mockOrderCommand, int id, ResponseCommon<bool> response)
        {
            mockOrderCommand.Setup(x => x.DeleteOrder(id)).ReturnsAsync(response);
        }

        #endregion

        #region Setup Methods Service

        public static void SetupGetAllOrderService(this Mock<IOrderService> mockOrderService, List<Order> orders)
        {
            mockOrderService.Setup(x => x.GetAllOrder()).ReturnsAsync(orders);
        }

        public static void SetupGetOrderByIdService(this Mock<IOrderService> mockOrderService, int id, Order order)
        {
            mockOrderService.Setup(x => x.GetOrderById(id)).ReturnsAsync(order);
        }

        public static void SetupCreateOrderService(this Mock<IOrderService> mockOrderService, Order createdOrder)
        {
            mockOrderService.Setup(x => x.CreateOrder(It.IsAny<Order>())).ReturnsAsync(createdOrder);
        }

        public static void SetupUpdateOrderService(this Mock<IOrderService> mockOrderService, Order updatedOrder)
        {
            mockOrderService.Setup(x => x.UpdateOrder(It.IsAny<Order>())).ReturnsAsync(updatedOrder);
        }

        public static void SetupDeleteOrderService(this Mock<IOrderService> mockOrderService, int id, bool success)
        {
            mockOrderService.Setup(x => x.DeleteOrder(id)).ReturnsAsync(success);
        }

        #endregion
    }
}
