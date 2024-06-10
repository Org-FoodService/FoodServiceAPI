using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodService.Models.Responses;
using FoodServiceAPI.Core.Command.Interface;
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
        [ProducesResponseType(typeof(ResponseCommon<bool>), 200)]
        public async Task<ActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            _logger.LogInformation("Attempting to sign up user...");
            var response = await _authCommand.SignUp(signUpDto);
            if (response.IsSuccess)
            {
                _logger.LogInformation("User signed up successfully.");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to sign up user: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Signs in a user.
        /// </summary>
        /// <param name="signInDTO">The sign-in data.</param>
        /// <returns>The single sign-on data.</returns>
        [AllowAnonymous]
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(ResponseCommon<SsoDto>), 200)]
        public async Task<ActionResult> SignIn([FromBody] SignInDto signInDTO)
        {
            _logger.LogInformation("Attempting to sign in user...");
            var response = await _authCommand.SignIn(signInDTO);
            if (response.IsSuccess)
            {
                _logger.LogInformation("User signed in successfully.");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to sign in user: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Adds a user to the admin role.
        /// </summary>
        /// <param name="userId">The ID of the user to add.</param>
        /// <returns>True if the user was added successfully.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("add-user-to-admin-role")]
        [ProducesResponseType(typeof(ResponseCommon<bool>), 200)]
        public async Task<ActionResult> AddUserToAdminRole([FromBody] int userId)
        {
            _logger.LogInformation($"Attempting to add user {userId} to admin role...");
            var response = await _authCommand.AddUserToAdminRole(userId);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"User {userId} added to admin role successfully.");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to add user {userId} to admin role: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Retrieves the current user.
        /// </summary>
        /// <returns>The current user.</returns>
        [HttpGet("get-current-user")]
        [ProducesResponseType(typeof(ResponseCommon<UserBase>), 200)]
        public async Task<ActionResult> GetCurrentUser()
        {
            _logger.LogInformation("Attempting to get current user...");
            var response = await _authCommand.GetCurrentUser();
            if (response.IsSuccess)
            {
                _logger.LogInformation("Current user retrieved successfully.");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to get current user: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Lists all users.
        /// </summary>
        /// <returns>The list of users.</returns>
        [HttpGet("list-users")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseCommon<List<ClientUser>>), 200)]
        public async Task<ActionResult> ListUsers()
        {
            _logger.LogInformation("Attempting to list users...");
            var response = await _authCommand.ListUsers();
            if (response.IsSuccess)
            {
                _logger.LogInformation("Users listed successfully.");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to list users: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user DTO.</returns>
        [HttpGet("get-userdto")]
        [ProducesResponseType(typeof(ResponseCommon<UserDto>), 200)]
        public async Task<ActionResult> GetUserDto([FromQuery] int id)
        {
            _logger.LogInformation($"Attempting to get user with ID: {id}...");
            var response = await _authCommand.GetUserDto(id);
            if (response.IsSuccess)
            {
                _logger.LogInformation($"User with ID: {id} retrieved successfully.");
                return Ok(response.Data);
            }
            else
            {
                _logger.LogError($"Failed to get user with ID {id}: {response.Message}");
                return StatusCode(response.StatusCode, response.Message);
            }
        }
    }
}
