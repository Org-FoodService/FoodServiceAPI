using FoodService.Models.Dto;
using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Core.Command.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodServiceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderCommand _orderCommand;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderCommand orderCommand, ILogger<OrderController> logger)
        {
            _orderCommand = orderCommand;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseCommon<List<Order>>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            _logger.LogInformation("Fetching all orders");
            var response = await _orderCommand.GetAllOrders();
            if (response.IsSuccess)
            {
                _logger.LogInformation("Successfully fetched all orders");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to fetch all orders: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseCommon<Order>), 200)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            _logger.LogInformation($"Fetching order with ID: {id}");
            var response = await _orderCommand.GetOrderById(id);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Successfully fetched order with ID: {id}");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to fetch order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseCommon<Order>), 201)]
        [ProducesResponseType(typeof(ResponseCommon<string>), 400)]
        [ProducesResponseType(typeof(ResponseCommon<string>), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            _logger.LogInformation("Creating a new order");

            try
            {
                var response = await _orderCommand.CreateOrder(orderDto);
                if (response.IsSuccess)
                {
                    _logger.LogInformation($"Order created successfully with ID: {response.Data.OrderId}");
                    return CreatedAtAction(nameof(GetOrderById), new { id = response.Data.OrderId }, response.Data);
                }
                else
                {
                    _logger.LogError($"Failed to create order, Error: {response.Message}");
                    return StatusCode(response.StatusCode, response.Message);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Failed to create order, Error: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the order.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseCommon<Order?>), 200)]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            _logger.LogInformation($"Updating order with ID: {id}");
            var response = await _orderCommand.UpdateOrder(id, order);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Order with ID: {id} updated successfully");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to update order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseCommon<bool>), 200)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _logger.LogInformation($"Deleting order with ID: {id}");
            var response = await _orderCommand.DeleteOrder(id);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Order with ID: {id} deleted successfully");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Failed to delete order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }
    }
}
