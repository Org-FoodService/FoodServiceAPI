using FoodService.Data.Context;
using FoodService.Core.Repository.Generic;
using FoodService.Core.Interface.Repository;
using FoodService.Nuget.Models;

namespace FoodService.Core.Repository
{
    /// <summary>
    /// Repository implementation for orders.
    /// </summary>
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {

        /// <summary>
        /// Initializes a new instance of the OrderRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public OrderRepository(AppDbContext context) : base(context) { }
    }
}
