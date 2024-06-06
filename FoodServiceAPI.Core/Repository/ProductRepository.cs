using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Repository;
using FoodServiceAPI.Core.Repository.Generic;
using FoodServiceAPI.Data.Context;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Core.Repository
{
    /// <summary>
    /// Repository implementation for products.
    /// </summary>
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        /// <summary>
        /// Initializes a new instance of the ProductRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
            : base(context, logger)
        {
        }
    }
}
