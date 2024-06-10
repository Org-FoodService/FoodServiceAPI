using FoodService.Models.Entities;
using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Data.SqlServer.Repository
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        /// <summary>
        /// Initializes a new instance of the OrderRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance.</param>
        public OrderRepository(AppDbContext context, ILogger<OrderRepository> logger)
            : base(context, logger)
        {
        }
    }
}
