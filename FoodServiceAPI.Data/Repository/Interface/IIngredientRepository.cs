using FoodService.Models.Entities;

namespace FoodServiceAPI.Data.SqlServer.Repository.Interface
{
    /// <summary>
    /// Interface for the repository of ingredients.
    /// </summary>
    public interface IIngredientRepository : IGenericRepository<Ingredient, int>
    {
    }
}
