using FoodService.Models.Auth.User;

namespace FoodServiceAPI.Data.SqlServer.Repository.Interface
{
    /// <summary>
    /// Interface for the repository of users.
    /// </summary>
    public interface IUserRepository : IGenericRepository<ClientUser, int>
    {
    }
}
