using FoodService.Data.Context;
using FoodService.Core.Repository.Generic;
using FoodService.Core.Interface.Repository;
using FoodService.Nuget.Models.Auth.User;

namespace FoodService.Core.Repository
{
    /// <summary>
    /// Repository implementation for user entities.
    /// </summary>
    public class UserRepository : GenericRepository<ClientUser, int>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
