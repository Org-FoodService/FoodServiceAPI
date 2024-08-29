using FoodService.Models.Dto;
using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Core.Command.Interface;
using FoodServiceAPI.Core.Service.Interface;

namespace FoodServiceAPI.Core.Command
{ 
    /// <summary>
    /// Command implementation for Order-related operations.
    /// </summary>
    public class OrderCommand(IOrderService orderService, IAuthService authService, IProductService productService, IIngredientService ingredientService) : IOrderCommand
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IAuthService _authService = authService;
        private readonly IProductService _productService = productService;
        private readonly IIngredientService _ingredientService = ingredientService;

        /// <summary>
        /// Retrieves all Orders.
        /// </summary>
        /// <returns>A response containing a list of Orders.</returns>
        public async Task<ResponseCommon<List<Order>>> GetAllOrders()
        {
            var Orders = await _orderService.GetAllOrder();
            return ResponseCommon<List<Order>>.Success(Orders);
        }

        /// <summary>
        /// Retrieves a Order by its ID.
        /// </summary>
        /// <param name="id">The ID of the Order to retrieve.</param>
        /// <returns>A response containing the Order.</returns>
        public async Task<ResponseCommon<Order>> GetOrderById(int id)
        {
            var Order = await _orderService.GetOrderById(id);
            if (Order == null)
            {
                return ResponseCommon<Order>.Failure("Order not found", 404);
            }
            return ResponseCommon<Order>.Success(Order);
        }

        /// <summary>
        /// Creates a new Order.
        /// </summary>
        /// <param name="orderDto">The Order to create.</param>
        /// <returns>A response containing the created Order.</returns>
        public async Task<ResponseCommon<Order>> CreateOrder(OrderDto orderDto)
        {
            // Get current user
            var currentUser = await _authService.GetCurrentUser();

            // List to hold all ingredient updates that need to happen
            var ingredientUpdates = new List<Ingredient>();

            // Validate product and ingredient availability
            foreach (var item in orderDto.OrderItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);

                if (product == null)
                {
                    return ResponseCommon<Order>.Failure($"Product with ID {item.ProductId} not found", 404);
                }

                if (!product.Active)
                {
                    return ResponseCommon<Order>.Failure($"Product with ID {item.ProductId} is inactive", 400);
                }

                // Validate ingredient availability
                foreach (var productIngredient in product.ProductIngredients ?? new List<ProductIngredient>())
                {
                    var ingredient = productIngredient.Ingredient;

                    if (ingredient == null)
                    {
                        return ResponseCommon<Order>.Failure($"Ingredient with ID {productIngredient.IngredientId} not found", 404);
                    }

                    if (ingredient.StockQuantity < item.Quantity)
                    {
                        return ResponseCommon<Order>.Failure($"Not enough stock for ingredient with ID {ingredient.Id} for product ID {item.ProductId}", 400);
                    }

                    // Add the ingredient to the list of updates
                    ingredient.StockQuantity -= item.Quantity;
                    ingredientUpdates.Add(ingredient);
                }
            }

            // If all validations passed, proceed with stock updates
            foreach (var ingredient in ingredientUpdates)
            {
                await _ingredientService.UpdateIngredientAsync(ingredient);
            }

            // Now proceed with creating the order
            var createdOrder = await _orderService.CreateOrder(orderDto, currentUser);

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

            var result = await _orderService.UpdateOrder(Order);

            return ResponseCommon<Order?>.Success(result);
        }

        /// <summary>
        /// Deletes a Order.
        /// </summary>
        /// <param name="id">The ID of the Order to delete.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<ResponseCommon<bool>> DeleteOrder(int id)
        {
            var existingOrder = await _orderService.GetOrderById(id);
            if (existingOrder == null)
            {
                return ResponseCommon<bool>.Failure("Order not found", 404);
            }

            _ = await _orderService.DeleteOrder(id);

            return ResponseCommon<bool>.Success(true);
        }
    }
}