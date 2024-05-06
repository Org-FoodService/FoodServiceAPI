using FoodService.Core.Interface.Repository.Generic;
using FoodService.Nuget.Models;

namespace FoodService.Core.Interface.Repository
{
    /// <summary>
    /// Interface for the repository of orders.
    /// </summary>
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
    }
}
