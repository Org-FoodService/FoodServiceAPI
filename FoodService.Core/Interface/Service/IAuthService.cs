using FoodService.Core.Dto;
using FoodService.Nuget.Models.Auth.User;

namespace FoodService.Core.Interface.Service
{
    /// <summary>
    /// Interface for the authentication service.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Retrieves a list of users.
        /// </summary>
        Task<List<ClientUser>> ListUsers();

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        Task<ClientUser> GetUserById(int userId);

        /// <summary>
        /// Retrieves user data transfer object by user ID.
        /// </summary>
        Task<UserDto> GetUserDto(int userId);

        /// <summary>
        /// Updates a user.
        /// </summary>
        Task<int> UpdateUser(ClientUser user);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        Task<bool> DeleteUser(int userId);

        /// <summary>
        /// Signs up a new user.
        /// </summary>
        Task<bool> SignUp(SignUpDto signUpDto);

        /// <summary>
        /// Signs in a user.
        /// </summary>
        Task<SsoDto> SignIn(SignInDto signInDto);

        /// <summary>
        /// Adds a user to the admin role.
        /// </summary>
        Task AddUserToAdminRole(int userId);

        /// <summary>
        /// Retrieves the current logged-in user.
        /// </summary>
        Task<UserBase> GetCurrentUser();
    }
}
