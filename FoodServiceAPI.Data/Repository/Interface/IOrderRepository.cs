using FoodService.Models.Entities;

namespace FoodServiceAPI.Data.SqlServer.Repository.Interface
{
    /// <summary>
    /// Interface for the repository of orders.
    /// </summary>
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
    }
}
