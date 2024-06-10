using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Core.Command.Interface;
using FoodServiceAPI.Core.Service.Interface;

namespace FoodServiceAPI.Core.Command
{ /// <summary>
  /// Command implementation for Order-related operations.
  /// </summary>
    public class OrderCommand(IOrderService OrderService) : IOrderCommand
    {
        private readonly IOrderService _OrderService = OrderService;

        /// <summary>
        /// Retrieves all Orders.
        /// </summary>
        /// <returns>A response containing a list of Orders.</returns>
        public async Task<ResponseCommon<List<Order>>> GetAllOrders()
        {
            var Orders = await _OrderService.GetAllOrder();
            return ResponseCommon<List<Order>>.Success(Orders);
        }

        /// <summary>
        /// Retrieves a Order by its ID.
        /// </summary>
        /// <param name="id">The ID of the Order to retrieve.</param>
        /// <returns>A response containing the Order.</returns>
        public async Task<ResponseCommon<Order>> GetOrderById(int id)
        {
            var Order = await _OrderService.GetOrderById(id);
            if (Order == null)
            {
                return ResponseCommon<Order>.Failure("Order not found", 404);
            }
            return ResponseCommon<Order>.Success(Order);
        }

        /// <summary>
        /// Creates a new Order.
        /// </summary>
        /// <param name="Order">The Order to create.</param>
        /// <returns>A response containing the created Order.</returns>
        public async Task<ResponseCommon<Order>> CreateOrder(Order Order)
        {
            var createdOrder = await _OrderService.CreateOrder(Order);
            return await GetOrderById(createdOrder.OrderId);
        }

        /// <summary>
        /// Updates an existing Order.
        /// </summary>
        /// <param name="id">The ID of the Order to update.</param>
        /// <param name="Order">The updated Order data.</param>
        /// <returns>A response containing the updated Order.</returns>
        public async Task<ResponseCommon<Order?>> UpdateOrder(int id, Order Order)
        {
            if (id != Order.OrderId)
            {
                return ResponseCommon<Order?>.Failure("The Order ID in the URL does not match the Order ID in the request body", 400);
            }

            var result = await _OrderService.UpdateOrder(Order);

            return ResponseCommon<Order?>.Success(result);
        }

        /// <summary>
        /// Deletes a Order.
        /// </summary>
        /// <param name="id">The ID of the Order to delete.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<ResponseCommon<bool>> DeleteOrder(int id)
        {
            var existingOrder = await _OrderService.GetOrderById(id);
            if (existingOrder == null)
            {
                return ResponseCommon<bool>.Failure("Order not found", 404);
            }

            _ = await _OrderService.DeleteOrder(id);

            return ResponseCommon<bool>.Success(true);
        }
    }
}