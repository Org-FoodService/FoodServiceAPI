using FoodServiceAPI.Core.Wrapper.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Wraps the UserManager class to provide custom functionality.
/// </summary>
/// <typeparam name="TUser">The type representing a user.</typeparam>
public class UserManagerWrapper<TUser> : IUserManagerWrapper<TUser> where TUser : class
{
    public readonly UserManager<TUser> Identity;

    /// <summary>
    /// Initializes a new instance of the UserManagerWrapper class.
    /// </summary>
    /// <param name="userManager">The instance of UserManager to wrap.</param>
    public UserManagerWrapper(UserManager<TUser> userManager)
    {
        Identity = userManager;
    }

    /// <summary>
    /// Counts the number of registered users in the system.
    /// </summary>
    /// <returns>The number of registered users.</returns>
    public async Task<int> CountUsersAsync()
    {
        // Get the count of registered users
        var userCount = await Identity.Users.CountAsync();

        return userCount;
    }
}
