using FoodService.Models.Auth.Role;
using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodServiceAPI.Core.Service.Interface;
using FoodServiceAPI.Core.Wrapper.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace FoodServiceAPI.Core.Service
{
    /// <summary>
    /// Service implementation for authentication-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserManagerWrapper<UserBase> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="userRepository">User repository instance.</param>
        /// <param name="configuration">Configuration instance.</param>
        /// <param name="userManager">User manager wrapper instance.</param>
        /// <param name="httpContextAccessor">HTTP context accessor instance.</param>
        public AuthService(
            ILogger<AuthService> logger,
            IUserRepository userRepository,
            IConfiguration configuration,
            IUserManagerWrapper<UserBase> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<List<ClientUser>> ListUsers()
        {
            try
            {
                List<ClientUser> listUsers = await _userRepository.ListAll().ToListAsync();
                return listUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while listing users.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        public async Task<ClientUser> GetUserById(int userId)
        {
            try
            {
                ClientUser user = await _userRepository.GetByIdAsync(userId);
                return user ?? throw new ArgumentException("User does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving user by ID: {userId}.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a user DTO by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve the DTO for.</param>
        /// <returns>The DTO representing the user with the specified ID.</returns>
        public async Task<UserDto> GetUserDto(int userId)
        {
            try
            {
                var user = await GetUserById(userId);
                UserDto userDto = new(user);
                return userDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving user DTO by ID: {userId}.");
                throw;
            }
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<int> UpdateUser(ClientUser user)
        {
            try
            {
                ClientUser findUser = await _userRepository.GetByIdAsync(user.Id) ?? throw new ArgumentException("User not found.");
                findUser.Email = user.Email;
                findUser.UserName = user.UserName;
                return await _userRepository.UpdateAsync(findUser, findUser.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating user: {user.Id}.");
                throw;
            }
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>True if the user was deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                ClientUser findUser = await _userRepository.GetByIdAsync(userId) ?? throw new ArgumentException("User not found.");
                await _userRepository.DeleteAsync(findUser);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting user: {userId}.");
                throw;
            }
        }

        /// <summary>
        /// Signs up a new user.
        /// </summary>
        /// <param name="signUpDto">The DTO containing sign up information.</param>
        /// <returns>True if sign up was successful; otherwise, false.</returns>
        public async Task<bool> SignUp(SignUpDto signUpDto)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(signUpDto.Username);
                if (userExists != null)
                    throw new ArgumentException("Username already exists");

                userExists = await _userManager.FindByEmailAsync(signUpDto.Email);
                if (userExists != null)
                    throw new ArgumentException("Email already exists");

                ClientUser user = new()
                {
                    CpfCnpj = signUpDto.CpfCnpj,
                    Email = signUpDto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = signUpDto.Username,
                    PhoneNumber = signUpDto.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, signUpDto.Password);

                if (!result.Succeeded)
                    if (result.Errors.ToList().Count > 0)
                        throw new ArgumentException(result.Errors.ToList()[0].Description);
                    else
                        throw new ArgumentException("User registration failed.");

                // If this is the first user, add admin role
                var isFirstUser = await _userManager.CountUsersAsync() == 1;
                if (isFirstUser)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                    if (!roleResult.Succeeded)
                    {
                        throw new ArgumentException("Failed to add user to admin role.");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user sign up.");
                throw;
            }
        }

        /// <summary>
        /// Adds a user to the administrator role.
        /// </summary>
        /// <param name="userId">The ID of the user to add to the administrator role.</param>
        public async Task AddUserToAdminRole(int userId)
        {
            try
            {
                var boolProperties = typeof(ApplicationRole).GetProperties().Where(p => p.PropertyType == typeof(bool));
                await AddUserToRoleAsync(userId, "Admin", boolProperties);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding user {userId} to admin role.");
                throw;
            }
        }

        private async Task AddUserToRoleAsync(int userId, string roleName, IEnumerable<PropertyInfo>? propertyInfos = null)
        {
            try
            {
                UserBase user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException("User not found.");
                await _userManager.AddToRoleAsync(user, roleName);

                //if (propertyInfos != null)
                //{
                //    // Iterate over the boolean properties and set them to true for the user
                //    foreach (var property in propertyInfos)
                //    {
                //        property.SetValue(property, true);
                //    }
                //}

                // Update the user in the database
                await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding user {userId} to role {roleName}.");
                throw;
            }
        }

        /// <summary>
        /// Signs in a user.
        /// </summary>
        /// <param name="signInDto">The DTO containing sign in information.</param>
        /// <returns>The SSO DTO containing the authentication token and user information.</returns>
        public async Task<SsoDto> SignIn(SignInDto signInDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(signInDto.Username) ?? throw new ArgumentException("User not found.");

                if (!await _userManager.CheckPasswordAsync(user, signInDto.Password))
                    throw new ArgumentException("Invalid password.");

                var userRolesList = (await _userManager.GetRolesAsync(user)).ToList();

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName!),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                authClaims.AddRange(userRolesList.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]!));
                var expiresTime = DateTime.UtcNow.AddHours(3);

                var token = new JwtSecurityToken(
                    issuer: _configuration["ValidIssuer"],
                    audience: _configuration["ValidAudience"],
                    expires: expiresTime,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new SsoDto(new JwtSecurityTokenHandler().WriteToken(token), expiresTime, userRolesList, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user sign in.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves the currently authenticated user.
        /// </summary>
        /// <returns>The currently authenticated user.</returns>
        public async Task<UserBase> GetCurrentUser()
        {
            try
            {
                UserBase user = (await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User))!;
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving the current user.");
                throw;
            }
        }
    }
}
