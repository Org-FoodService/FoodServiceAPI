using FoodService.Models;
using FoodServiceAPI.Core.Interface.Repository.Generic;

namespace FoodServiceAPI.Core.Interface.Repository
{
    /// <summary>
    /// Interface for the repository of orders.
    /// </summary>
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
    }
}
