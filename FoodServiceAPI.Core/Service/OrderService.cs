using FoodService.Models.Entities;
using FoodServiceAPI.Core.Service.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceAPI.Core.Service
{
    /// <summary>
    /// Service implementation for order-related operations.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        /// <summary>
        /// Initializes a new instance of the ProductService class.
        /// </summary>
        /// <param name="repository">The product repository.</param>
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="Order">The Order to create.</param>
        /// <returns>The created Order.</returns>
        public async Task<Order> CreateOrder(Order Order)
        {
            return await _repository.CreateAsync(Order);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return false;

            return await _repository.DeleteAsync(order, order.OrderId);
        }

        public async Task<List<Order>> GetAllOrder()
        {
            return await _repository.ListAll().ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Order?> UpdateOrder(Order Order)
        {
            var existingOrder = await _repository.GetByIdAsync(Order.OrderId);
            if (existingOrder == null)
                return null;

            existingOrder.OrderItems = Order.OrderItems;

            await _repository.UpdateAsync(existingOrder, existingOrder.OrderId);
            return existingOrder;
        }
    }
    }
