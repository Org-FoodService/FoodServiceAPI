using FoodService.Core.Interface.Command;
using FoodService.Core.Interface.Service;
using FoodService.Nuget.Models;

namespace FoodService.Core.Command
{
    /// <summary>
    /// Command implementation for product-related operations.
    /// </summary>
    public class ProductCommand(IProductService productService) : IProductCommand
    {
        private readonly IProductService _productService = productService;

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A response containing a list of products.</returns>
        public async Task<ResponseCommon<List<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return ResponseCommon<List<Product>>.Success(products);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>A response containing the product.</returns>
        public async Task<ResponseCommon<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return ResponseCommon<Product>.Failure("Product not found", 404);
            }
            return ResponseCommon<Product>.Success(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>A response containing the created product.</returns>
        public async Task<ResponseCommon<Product>> CreateProduct(Product product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            return await GetProductById(createdProduct.Id);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>A response containing the updated product.</returns>
        public async Task<ResponseCommon<Product?>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return ResponseCommon<Product?>.Failure("The Product ID in the URL does not match the Product ID in the request body", 400);
            }

            var result = await _productService.UpdateProductAsync(product);

            return ResponseCommon<Product?>.Success(result);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<ResponseCommon<bool>> DeleteProduct(int id)
        {
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return ResponseCommon<bool>.Failure("Product not found", 404);
            }

            _ = await _productService.DeleteProductAsync(id);

            return ResponseCommon<bool>.Success(true);
        }
    }
}
