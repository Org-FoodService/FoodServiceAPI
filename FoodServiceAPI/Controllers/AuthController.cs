using FoodService.Models.Dto;
using FoodServiceAPI.Core.Interface.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodServiceAPI.Controllers
{
    /// <summary>
    /// Controller for authentication-related operations.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthCommand _authCommand;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authCommand">The authentication command service.</param>
        /// <param name="logger">The logger.</param>
        public AuthController(IAuthCommand authCommand, ILogger<AuthController> logger)
        {
            _authCommand = authCommand;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="signUpDto">The sign up data.</param>
        /// <returns>The result of the sign-up operation.</returns>
        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            try
            {
                _logger.LogInformation("Attempting to sign up user...");
                var ret = await _authCommand.SignUp(signUpDto);
                _logger.LogInformation("User signed up successfully.");
                return Ok(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sign up.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Signs in a user.
        /// </summary>
        /// <param name="signInDTO">The sign-in data.</param>
        /// <returns>The single sign-on data.</returns>
        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn([FromBody] SignInDto signInDTO)
        {
            try
            {
                _logger.LogInformation("Attempting to sign in user...");
                var ssoDTO = await _authCommand.SignIn(signInDTO);
                _logger.LogInformation("User signed in successfully.");
                return Ok(ssoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sign in.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adds a user to the admin role.
        /// </summary>
        /// <param name="userId">The ID of the user to add.</param>
        /// <returns>True if the user was added successfully.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("add-user-to-admin-role")]
        public async Task<ActionResult> AddUserToAdminRole([FromBody] int userId)
        {
            try
            {
                _logger.LogInformation($"Attempting to add user {userId} to admin role...");
                await _authCommand.AddUserToAdminRole(userId);
                _logger.LogInformation($"User {userId} added to admin role successfully.");
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding user {userId} to admin role.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the current user.
        /// </summary>
        /// <returns>The current user.</returns>
        [HttpGet("get-current-user")]
        public async Task<ActionResult> GetCurrentUser()
        {
            try
            {
                _logger.LogInformation("Attempting to get current user...");
                var currentUser = await _authCommand.GetCurrentUser();
                _logger.LogInformation("Current user retrieved successfully.");
                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting current user.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lists all users.
        /// </summary>
        /// <returns>The list of users.</returns>
        [HttpGet("list-users")]
        public async Task<ActionResult> ListUsers()
        {
            try
            {
                _logger.LogInformation("Attempting to list users...");
                var list = await _authCommand.ListUsers();
                _logger.LogInformation("Users listed successfully.");
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while listing users.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user DTO.</returns>
        [HttpGet("get-userdto")]
        public async Task<ActionResult> GetUserDTO([FromQuery] int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get user with ID: {id}...");
                var user = await _authCommand.GetUserDto(id);
                _logger.LogInformation($"User with ID: {id} retrieved successfully.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting user with ID: {id}.");
                return BadRequest(ex.Message);
            }
        }
    }
}
