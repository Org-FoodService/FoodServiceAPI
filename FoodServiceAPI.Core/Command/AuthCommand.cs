using FoodService.Models;
using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodServiceAPI.Core.Interface.Command;
using FoodServiceAPI.Core.Interface.Service;
using Microsoft.AspNetCore.Authorization;

namespace FoodServiceAPI.Core.Command
{
    /// <summary>
    /// Command implementation for authentication-related operations.
    /// </summary>
    public class AuthCommand(IAuthService authService) : IAuthCommand
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="signUpDto">The sign-up data.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<ResponseCommon<bool>> SignUp(SignUpDto signUpDto)
        {
            try
            {
                bool ret = await _authService.SignUp(signUpDto);

                return ResponseCommon<bool>.Success(ret);
            }
            catch (ArgumentException ex)
            {
                return ResponseCommon<bool>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ResponseCommon<bool>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="signInDTO">The sign-in data.</param>
        /// <returns>A response containing the authentication token.</returns>
        [AllowAnonymous]
        public async Task<ResponseCommon<SsoDto>> SignIn(SignInDto signInDTO)
        {
            try
            {
                SsoDto ssoDTO = await _authService.SignIn(signInDTO);

                return ResponseCommon<SsoDto>.Success(ssoDTO);
            }
            catch (ArgumentException ex)
            {
                return ResponseCommon<SsoDto>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ResponseCommon<SsoDto>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Adds a user to the admin role.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<ResponseCommon<bool>> AddUserToAdminRole(int userId)
        {
            try
            {
                await _authService.AddUserToAdminRole(userId);

                return ResponseCommon<bool>.Success(true);
            }
            catch (ArgumentException ex)
            {
                return ResponseCommon<bool>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ResponseCommon<bool>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Gets the current logged-in user.
        /// </summary>
        /// <returns>A response containing the current user.</returns>
        public async Task<ResponseCommon<UserBase>> GetCurrentUser()
        {
            try
            {
                var currentUser = await _authService.GetCurrentUser();

                return ResponseCommon<UserBase>.Success(currentUser);
            }
            catch (ArgumentException ex)
            {
                return ResponseCommon<UserBase>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ResponseCommon<UserBase>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Lists all users.
        /// </summary>
        /// <returns>A response containing a list of users.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<ResponseCommon<List<ClientUser>>> ListUsers()
        {
            try
            {
                List<ClientUser> list = await _authService.ListUsers();

                return ResponseCommon<List<ClientUser>>.Success(list);
            }
            catch (ArgumentException ex)
            {
                return ResponseCommon<List<ClientUser>>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ResponseCommon<List<ClientUser>>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Gets the DTO of a user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A response containing the user DTO.</returns>
        [Authorize(Roles = "Employee")]
        public async Task<ResponseCommon<UserDto>> GetUserDto(int id)
        {
            try
            {
                UserDto userDto = await _authService.GetUserDto(id);

                return ResponseCommon<UserDto>.Success(userDto);
            }
            catch (ArgumentException ex)
            {
                return ResponseCommon<UserDto>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                return ResponseCommon<UserDto>.Failure(ex.Message);
            }
        }
    }
}
