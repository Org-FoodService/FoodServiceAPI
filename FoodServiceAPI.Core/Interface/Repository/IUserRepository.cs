using FoodService.Models.Auth.User;
using FoodServiceAPI.Core.Interface.Repository.Generic;

namespace FoodServiceAPI.Core.Interface.Repository
{
    /// <summary>
    /// Interface for the repository of users.
    /// </summary>
    public interface IUserRepository : IGenericRepository<ClientUser, int>
    {
    }
}
