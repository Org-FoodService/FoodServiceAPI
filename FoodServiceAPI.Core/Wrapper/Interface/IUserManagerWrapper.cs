using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace FoodServiceAPI.Core.Wrapper.Interface
{
    /// <summary>
    /// Defines methods for managing users.
    /// </summary>
    /// <typeparam name="TUser">The type representing a user.</typeparam>
    public interface IUserManagerWrapper<TUser> where TUser : class
    {
        /// <summary>
        /// Counts the number of registered users in the system.
        /// </summary>
        /// <returns>The number of registered users.</returns>
        Task<int> CountUsersAsync();

        /// <summary>
        /// Finds a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to find.</param>
        /// <returns>The user with the specified username.</returns>
        Task<TUser> FindByNameAsync(string userName);

        /// <summary>
        /// Finds a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to find.</param>
        /// <returns>The user with the specified email address.</returns>
        Task<TUser> FindByEmailAsync(string email);

        /// <summary>
        /// Finds a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to find.</param>
        /// <returns>The user with the specified ID.</returns>
        Task<TUser> FindByIdAsync(string userId);

        /// <summary>
        /// Creates a new user with the specified password.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>The result of the user creation.</returns>
        Task<IdentityResult> CreateAsync(TUser user, string password);

        /// <summary>
        /// Adds a user to a specified role.
        /// </summary>
        /// <param name="user">The user to add to the role.</param>
        /// <param name="role">The name of the role.</param>
        /// <returns>The result of the role addition.</returns>
        Task<IdentityResult> AddToRoleAsync(TUser user, string role);

        /// <summary>
        /// Checks if the specified password is valid for the user.
        /// </summary>
        /// <param name="user">The user to check the password for.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        Task<bool> CheckPasswordAsync(TUser user, string password);

        /// <summary>
        /// Gets the roles for the specified user.
        /// </summary>
        /// <param name="user">The user to get roles for.</param>
        /// <returns>A list of roles for the user.</returns>
        Task<IList<string>> GetRolesAsync(TUser user);

        /// <summary>
        /// Gets the user associated with the specified principal.
        /// </summary>
        /// <param name="principal">The principal to get the user for.</param>
        /// <returns>The user associated with the principal.</returns>
        Task<TUser> GetUserAsync(ClaimsPrincipal principal);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The result of the update operation.</returns>
        Task<IdentityResult> UpdateAsync(TUser user);
    }
}
