using FoodService.Models;
using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodServiceAPI.Controllers
{
    /// <summary>
    /// Controller for managing Order operations.
    /// </summary>
    ///     [Authorize]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderCommand _OrderCommand;
        private readonly ILogger<OrderController> _logger;

        /// <summary>
        /// Constructor for OrderController.
        /// </summary>
        /// <param name="OrderCommand">The Order command service.</param>
        /// <param name="logger">The logger service.</param>
        public OrderController(IOrderCommand OrderCommand, ILogger<OrderController> logger)
        {
            _OrderCommand = OrderCommand;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all Orders.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            _logger.LogInformation("Fetching all Orders");
            var response = await _OrderCommand.GetAllOrders();
            if (response.IsSuccess)
            {
                _logger.LogInformation("Successfully fetched all Orders");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to fetch all Orders: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Retrieves a Order by its ID.
        /// </summary>
        /// <param name="id">The ID of the Order to retrieve.</param>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            _logger.LogInformation($"Fetching Order with ID: {id}");
            var response = await _OrderCommand.GetOrderById(id);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Successfully fetched Order with ID: {id}");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to fetch Order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Creates a new Order.
        /// </summary>
        /// <param name="Order">The Order to create.</param>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order Order)
        {
            _logger.LogInformation("Creating a new Order");
            var response = await _OrderCommand.CreateOrder(Order);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Order created successfully with ID: {response.Data.OrderId}");
                return CreatedAtAction(nameof(GetOrderById), new { id = response.Data.OrderId }, response.Data);
            }
            else
            {
                _logger.LogError($"Failed to create Order, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Updates an existing Order.
        /// </summary>
        /// <param name="id">The ID of the Order to update.</param>
        /// <param name="Order">The updated Order data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order Order)
        {
            _logger.LogInformation($"Updating Order with ID: {id}");
            var response = await _OrderCommand.UpdateOrder(id, Order);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Order with ID: {id} updated successfully");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to update Order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Deletes a Order.
        /// </summary>
        /// <param name="id">The ID of the Order to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _logger.LogInformation($"Deleting Order with ID: {id}");
            var response = await _OrderCommand.DeleteOrder(id);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Order with ID: {id} deleted successfully");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Failed to delete Order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }
    }
}
