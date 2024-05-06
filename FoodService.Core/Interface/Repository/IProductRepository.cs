using FoodService.Core.Interface.Repository.Generic;
using FoodService.Nugget.Models;

namespace FoodService.Core.Interface.Repository
{
    /// <summary>
    /// Interface for the repository of products.
    /// </summary>
    public interface IProductRepository : IGenericRepository<Product, int>
    {
    }
}
