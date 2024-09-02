using FoodService.Models.Dto;
using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Controllers.SwaggerRequestExample;
using FoodServiceAPI.Core.Command.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace FoodServiceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderCommand orderCommand, ILogger<OrderController> logger) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseCommon<List<Order>>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            logger.LogInformation("Fetching all orders");
            var response = await orderCommand.GetAllOrders();
            if (response.IsSuccess)
            {
                logger.LogInformation("Successfully fetched all orders");
                return Ok(response.Data);
            }
            else
            {
                logger.LogError($"Failed to fetch all orders: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseCommon<Order>), 200)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            logger.LogInformation($"Fetching order with ID: {id}");
            var response = await orderCommand.GetOrderById(id);
            if (response.IsSuccess)
            {
                logger.LogInformation($"Successfully fetched order with ID: {id}");
                return Ok(response.Data);
            }
            else
            {
                logger.LogError($"Failed to fetch order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(OrderDto), typeof(OrderDtoExample))]
        [ProducesResponseType(typeof(ResponseCommon<Order>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            logger.LogInformation("Creating a new order");

            try
            {
                var response = await orderCommand.CreateOrder(orderDto);
                if (response.IsSuccess)
                {
                    logger.LogInformation($"Order created successfully with ID: {response.Data.Id}");
                    return CreatedAtAction(nameof(GetOrderById), new { id = response.Data.Id }, response.Data);
                }
                else
                {
                    logger.LogError($"Failed to create order, Error: {response.Message}");
                    return StatusCode(response.StatusCode, response.Message);
                }
            }

            catch (Exception ex)
            {
                logger.LogError($"Failed to create order, Error: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the order.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseCommon<Order?>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            logger.LogInformation($"Updating order with ID: {id}");
            var response = await orderCommand.UpdateOrder(id, order);
            if (response.IsSuccess)
            {
                logger.LogInformation($"Order with ID: {id} updated successfully");
                return Ok(response.Data);
            }
            else
            {
                logger.LogError($"Failed to update order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseCommon<bool>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            logger.LogInformation($"Deleting order with ID: {id}");
            var response = await orderCommand.DeleteOrder(id);
            if (response.IsSuccess)
            {
                logger.LogInformation($"Order with ID: {id} deleted successfully");
                return NoContent();
            }
            else
            {
                logger.LogError($"Failed to delete order with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }
    }
}
