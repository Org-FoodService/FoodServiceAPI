using System.Security.Claims;
using FoodServiceAPI.Core.Wrapper.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceAPI.Core.Wrapper
{
    /// <summary>
    /// Wraps the UserManager class to provide custom functionality.
    /// </summary>
    /// <typeparam name="TUser">The type representing a user.</typeparam>
    public class UserManagerWrapper<TUser> : IUserManagerWrapper<TUser> where TUser : class
    {
        // Creating a wrapper class that merely delegates calls to the UserManager might seem redundant at first glance,
        // but there are several important reasons to adopt this approach:
        //
        // 1 - Ease of Testing
        // By using an Wrapper, it is possible to create mock or stub implementations for unit tests.
        // This allows you to test the AuthService in isolation without depending on the real UserManager implementation,
        // making it easier to create more robust and faster tests.
        //
        // 2 - Single Responsibility Principle (SRP)
        // Following the single responsibility principle means that each class should have a single responsibility.
        // The UserManager is responsible for managing users,
        // while the wrapper can be responsible for adapting the UserManager interface to meet the specific needs of the application,
        // maintaining clear and separate responsibilities.

        private readonly UserManager<TUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the UserManagerWrapper class.
        /// </summary>
        /// <param name="userManager">The instance of UserManager to wrap.</param>
        public UserManagerWrapper(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Counts the number of registered users in the system.
        /// </summary>
        /// <returns>The number of registered users.</returns>
        public async Task<int> CountUsersAsync()
        {
            return await _userManager.Users.CountAsync();
        }

        /// <summary>
        /// Finds a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to find.</param>
        /// <returns>The user with the specified username.</returns>
        public async Task<TUser> FindByNameAsync(string userName)
        {
            return (await _userManager.FindByNameAsync(userName))!;
        }

        /// <summary>
        /// Finds a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to find.</param>
        /// <returns>The user with the specified email address.</returns>
        public async Task<TUser> FindByEmailAsync(string email)
        {
            return (await _userManager.FindByEmailAsync(email))!;
        }

        /// <summary>
        /// Finds a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to find.</param>
        /// <returns>The user with the specified ID.</returns>
        public async Task<TUser> FindByIdAsync(string userId)
        {
            return (await _userManager.FindByIdAsync(userId))!;
        }

        /// <summary>
        /// Creates a new user with the specified password.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>The result of the user creation.</returns>
        public async Task<IdentityResult> CreateAsync(TUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        /// <summary>
        /// Adds a user to a specified role.
        /// </summary>
        /// <param name="user">The user to add to the role.</param>
        /// <param name="role">The name of the role.</param>
        /// <returns>The result of the role addition.</returns>
        public async Task<IdentityResult> AddToRoleAsync(TUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        /// <summary>
        /// Checks if the specified password is valid for the user.
        /// </summary>
        /// <param name="user">The user to check the password for.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        public async Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        /// <summary>
        /// Gets the roles for the specified user.
        /// </summary>
        /// <param name="user">The user to get roles for.</param>
        /// <returns>A list of roles for the user.</returns>
        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        /// <summary>
        /// Gets the user associated with the specified principal.
        /// </summary>
        /// <param name="principal">The principal to get the user for.</param>
        /// <returns>The user associated with the principal.</returns>
        public async Task<TUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return (await _userManager.GetUserAsync(principal))!;
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The result of the update operation.</returns>
        public async Task<IdentityResult> UpdateAsync(TUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
