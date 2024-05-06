using FoodService.Core.Interface.Command;
using FoodService.Nugget.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Api.Controllers
{
    /// <summary>
    /// Controller for managing product operations.
    /// </summary>
    ///     [Authorize]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductCommand _productCommand;
        private readonly ILogger<ProductController> _logger;

        /// <summary>
        /// Constructor for ProductController.
        /// </summary>
        /// <param name="productCommand">The product command service.</param>
        /// <param name="logger">The logger service.</param>
        public ProductController(IProductCommand productCommand, ILogger<ProductController> logger)
        {
            _productCommand = productCommand;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("Fetching all products");
            var response = await _productCommand.GetAllProducts();
            if (response.IsSuccess)
            {
                _logger.LogInformation("Successfully fetched all products");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to fetch all products: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            _logger.LogInformation($"Fetching product with ID: {id}");
            var response = await _productCommand.GetProductById(id);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Successfully fetched product with ID: {id}");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to fetch product with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _logger.LogInformation("Creating a new product");
            var response = await _productCommand.CreateProduct(product);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Product created successfully with ID: {response.Data.Id}");
                return CreatedAtAction(nameof(GetProductById), new { id = response.Data.Id }, response.Data);
            }
            else
            {
                _logger.LogError($"Failed to create product, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            _logger.LogInformation($"Updating product with ID: {id}");
            var response = await _productCommand.UpdateProduct(id, product);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Product with ID: {id} updated successfully");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to update product with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation($"Deleting product with ID: {id}");
            var response = await _productCommand.DeleteProduct(id);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"Product with ID: {id} deleted successfully");
                return NoContent();
            }
            else
            {
                _logger.LogError($"Failed to delete product with ID: {id}, Error: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }
    }
}
