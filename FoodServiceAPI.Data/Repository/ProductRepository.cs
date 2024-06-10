using FoodService.Models.Entities;
using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository.Generic;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Data.SqlServer.Repository
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
