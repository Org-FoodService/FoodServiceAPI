using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodService.Models.Entities;
using FoodServiceAPI.Core.Service.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceAPI.Core.Service
{
    /// <summary>
    /// Service implementation for order-related operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the ProductService class.
    /// </remarks>
    /// <param name="repository">The product repository.</param>
    public class OrderService(IOrderRepository repository) : IOrderService
    {

        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="Order">The Order to create.</param>
        /// <returns>The created Order.</returns>
        public async Task<Order> CreateOrder(OrderDto orderDto, UserBase currentUser)
        {
            Order orderResult = new()
            {
                OrderItems = new(),
                User = currentUser
            };

            foreach (var item in orderDto.OrderItems)
            {
                orderResult.OrderItems.Add(
                    new()
                    {
                        Comment = item.Comment,
                        Quantity = item.Quantity,
                        ProductId = item.ProductId
                    }
                );
            }

            return await repository.CreateOrderWithTransactionAsync(orderResult);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await repository.GetByIdAsync(id);
            if (order == null)
                return false;

            return await repository.DeleteAsync(order, order.Id);
        }

        public async Task<List<Order>> GetAllOrder()
        {
            return await repository.ListAll().ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<Order?> UpdateOrder(Order Order)
        {
            var existingOrder = await repository.GetByIdAsync(Order.Id);
            if (existingOrder == null)
                return null;

            existingOrder.OrderItems = Order.OrderItems;

            await repository.UpdateAsync(existingOrder, existingOrder.Id);
            return existingOrder;
        }
    }
}
