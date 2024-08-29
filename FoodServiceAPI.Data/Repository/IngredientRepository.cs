using FoodService.Models.Entities;
using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Data.SqlServer.Repository
{
    public class IngredientRepository : GenericRepository<Ingredient, int>, IIngredientRepository
    {
        /// <summary>
        /// Initializes a new instance of the IngredientRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public IngredientRepository(AppDbContext context, ILogger<IngredientRepository> logger)
            : base(context, logger)
        {
        }
    }
}
