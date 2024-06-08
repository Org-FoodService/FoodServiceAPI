using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Repository.Generic;

namespace FoodServiceAPI.Core.Interface.Repository
{
    /// <summary>
    /// Interface for the repository of products.
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product, int>
    {
    }
}
