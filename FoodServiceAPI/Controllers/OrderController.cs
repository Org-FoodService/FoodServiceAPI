using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodServiceAPI.Controllers
{
    //[Authorize]
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
        public async Task<IActionResult> CreateOrder(Order order)
        {
            _logger.LogInformation("Creating a new order");

            // Ensure OrderItems have reference to Order
            foreach (var item in order.OrderItems)
            {
                item.Order = order;
            }

            var response = await _orderCommand.CreateOrder(order);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            _logger.LogInformation($"Updating order with ID: {id}");

            // Ensure OrderItems have reference to Order
            foreach (var item in order.OrderItems)
            {
                item.Order = order;
            }

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
