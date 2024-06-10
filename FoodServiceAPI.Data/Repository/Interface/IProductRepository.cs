using FoodService.Models.Entities;

namespace FoodServiceAPI.Data.SqlServer.Repository.Interface
{
    /// <summary>
    /// Interface for the repository of products.
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product, int>
    {
    }
}
