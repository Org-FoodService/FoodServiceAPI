using FoodService.Models.Entities;
using FoodServiceAPI.Core.Service.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceAPI.Core.Service
{
    /// <summary>
    /// Service implementation for product-related operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the ProductService class.
    /// </remarks>
    /// <param name="repository">The product repository.</param>
    public class ProductService(IProductRepository repository) : IProductService
    {

        /// <summary>
        /// Creates a new product asynchronously.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        public async Task<Product> CreateProductAsync(Product product)
        {
            return await repository.CreateAsync(product);
        }

        /// <summary>
        /// Deletes a product asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if deletion is successful, otherwise false.</returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null)
                return false;

            return await repository.DeleteAsync(product, product.Id);
        }

        /// <summary>
        /// Retrieves all products asynchronously.
        /// </summary>
        /// <returns>A list of all products.</returns>
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await repository.ListAll().ToListAsync();
        }

        /// <summary>
        /// Retrieves a product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The retrieved product.</returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Retrieves a product by its ID asynchronously, including the list of ingredients.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The retrieved product, including its ingredients.</returns>
        public async Task<Product> GetProductByIdIncludingIngredientsAsync(int id)
        {
            return await repository.GetByIdAsync(id, include: p => p.Include(p => p.ProductIngredients).ThenInclude(pi => pi.Ingredient));
        }

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product, or null if the product does not exist.</returns>
        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existingProduct = await repository.GetByIdAsync(product.Id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Type = product.Type;
            existingProduct.Active = product.Active;

            await repository.UpdateAsync(existingProduct, existingProduct.Id);
            return existingProduct;
        }
    }
}
