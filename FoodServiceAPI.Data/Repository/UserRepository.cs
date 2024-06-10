using FoodService.Models.Auth.User;
using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository.Generic;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Data.SqlServer.Repository
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
        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
            : base(context, logger)
        {
        }
    }
}
