using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Repository;
using FoodServiceAPI.Core.Repository.Generic;
using FoodServiceAPI.Data.Context;

namespace FoodServiceAPI.Core.Repository
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {

        /// <summary>
        /// Initializes a new instance of the OrderRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public OrderRepository(AppDbContext context) : base(context) { }
    }
}
