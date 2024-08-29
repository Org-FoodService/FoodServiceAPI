using FoodService.Models.Entities;
using FoodServiceAPI.Core.Service.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceAPI.Core.Service
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repository;

        /// <summary>
        /// Initializes a new instance of the IngredientService class.
        /// </summary>
        /// <param name="repository">The Ingredient repository.</param>
        public IngredientService(IIngredientRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new Ingredient asynchronously.
        /// </summary>
        /// <param name="Ingredient">The Ingredient to create.</param>
        /// <returns>The created Ingredient.</returns>
        public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
        {
            return await _repository.CreateAsync(ingredient);
        }

        /// <summary>
        /// Deletes a Ingredient asynchronously by its ID.
        /// </summary>
        /// <param name="id">The ID of the Ingredient to delete.</param>
        /// <returns>True if deletion is successful, otherwise false.</returns>
        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var Ingredient = await _repository.GetByIdAsync(id);
            if (Ingredient == null)
                return false;

            return await _repository.DeleteAsync(Ingredient);
        }

        /// <summary>
        /// Retrieves all Ingredients asynchronously.
        /// </summary>
        /// <returns>A list of all Ingredients.</returns>
        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _repository.ListAll().ToListAsync();
        }

        /// <summary>
        /// Retrieves a Ingredient by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the Ingredient to retrieve.</param>
        /// <returns>The retrieved Ingredient.</returns>
        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates an existing Ingredient asynchronously.
        /// </summary>
        /// <param name="Ingredient">The Ingredient to update.</param>
        /// <returns>The updated Ingredient, or null if the Ingredient does not exist.</returns>
        public async Task<Ingredient?> UpdateIngredientAsync(Ingredient ingredient)
        {
            var existingIngredient = await _repository.GetByIdAsync(ingredient.Id);
            if (existingIngredient == null)
                return null;

            existingIngredient.Name = ingredient.Name;
            existingIngredient.IsFresh = ingredient.IsFresh;
            existingIngredient.StockQuantity = ingredient.StockQuantity;
            existingIngredient.ShortDescription = ingredient.ShortDescription;
            existingIngredient.Description = ingredient.Description;
            existingIngredient.Image = ingredient.Image;

            await _repository.UpdateAsync(existingIngredient);
            return existingIngredient;
        }
    }
}
