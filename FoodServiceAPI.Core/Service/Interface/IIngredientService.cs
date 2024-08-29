using FoodService.Models.Entities;

namespace FoodServiceAPI.Core.Service.Interface
{
    public interface IIngredientService
    {
        Task<Ingredient> CreateIngredientAsync(Ingredient ingredient);
        Task<bool> DeleteIngredientAsync(int id);
        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<Ingredient?> UpdateIngredientAsync(Ingredient ingredient);
    }
}