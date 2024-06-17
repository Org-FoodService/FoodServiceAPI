using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodService.Models.Responses;
using FoodServiceApi.Tests.TestsBase;
using FoodServiceAPI.Controllers;
using FoodServiceAPI.Core.Command.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace FoodServiceApi.Tests.API.Controller
{
    [ExcludeFromCodeCoverage]
    public class AuthControllerTests : AuthTestHelper
    {
        private readonly AuthController _controller;
        private readonly Mock<IAuthCommand> _mockAuthCommand;
        private readonly Mock<ILogger<AuthController>> _mockLogger;

        public AuthControllerTests()
        {
            _mockAuthCommand = new Mock<IAuthCommand>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_mockAuthCommand.Object, _mockLogger.Object);
        }

        #region Setup Methods

        private void SetupSignUpCommand(SignUpDto signUpDto, ResponseCommon<bool> response)
        {
            _mockAuthCommand.Setup(x => x.SignUp(signUpDto)).ReturnsAsync(response);
        }

        private void SetupSignInCommand(SignInDto signInDto, ResponseCommon<SsoDto> response)
        {
            _mockAuthCommand.Setup(x => x.SignIn(signInDto)).ReturnsAsync(response);
        }

        private void SetupAddUserToAdminRoleCommand(int userId, ResponseCommon<bool> response)
        {
            _mockAuthCommand.Setup(x => x.AddUserToAdminRole(userId)).ReturnsAsync(response);
        }

        private void SetupGetCurrentUserCommand(ResponseCommon<UserBase> response)
        {
            _mockAuthCommand.Setup(x => x.GetCurrentUser()).ReturnsAsync(response);
        }

        private void SetupListUsersCommand(ResponseCommon<List<ClientUser>> response)
        {
            _mockAuthCommand.Setup(x => x.ListUsers()).ReturnsAsync(response);
        }

        private void SetupGetUserDtoCommand(int userId, ResponseCommon<UserDto> response)
        {
            _mockAuthCommand.Setup(x => x.GetUserDto(userId)).ReturnsAsync(response);
        }

        #endregion

        [Fact(DisplayName = "SignUp - Success - Returns Ok with true value")]
        public async Task SignUp_Success_ReturnsOk()
        {
            // Arrange
            var signUpDto = SignUpDto;
            var response = ResponseCommon<bool>.Success(true);
            SetupSignUpCommand(signUpDto, response);

            // Act
            var result = await _controller.SignUp(signUpDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value!);
        }

        [Fact(DisplayName = "SignUp - Failure - Returns Status Code 400 with error message")]
        public async Task SignUp_Failure_ReturnsStatusCode()
        {
            // Arrange
            var signUpDto = SignUpDto;
            var response = ResponseCommon<bool>.Failure("Sign up failed", 400);
            SetupSignUpCommand(signUpDto, response);

            // Act
            var result = await _controller.SignUp(signUpDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Sign up failed", statusCodeResult.Value);
        }

        [Fact(DisplayName = "SignIn - Success - Returns Ok with SsoDto")]
        public async Task SignIn_Success_ReturnsOk()
        {
            // Arrange
            var signInDto = SignInDto;
            var ssoDto = SsoDto;
            var response = ResponseCommon<SsoDto>.Success(ssoDto);
            SetupSignInCommand(signInDto, response);

            // Act
            var result = await _controller.SignIn(signInDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ssoDto, okResult.Value);
        }

        [Fact(DisplayName = "SignIn - Failure - Returns Status Code 401 with error message")]
        public async Task SignIn_Failure_ReturnsStatusCode()
        {
            // Arrange
            var signInDto = SignInDto;
            var response = ResponseCommon<SsoDto>.Failure("Sign in failed", 401);
            SetupSignInCommand(signInDto, response);

            // Act
            var result = await _controller.SignIn(signInDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, statusCodeResult.StatusCode);
            Assert.Equal("Sign in failed", statusCodeResult.Value);
        }

        [Fact(DisplayName = "AddUserToAdminRole - Success - Returns Ok with true value")]
        public async Task AddUserToAdminRole_Success_ReturnsOk()
        {
            // Arrange
            int userId = 1;
            var response = ResponseCommon<bool>.Success(true);
            SetupAddUserToAdminRoleCommand(userId, response);

            // Act
            var result = await _controller.AddUserToAdminRole(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value!);
        }

        [Fact(DisplayName = "AddUserToAdminRole - Failure - Returns Status Code 400 with error message")]
        public async Task AddUserToAdminRole_Failure_ReturnsStatusCode()
        {
            // Arrange
            int userId = 1;
            var response = ResponseCommon<bool>.Failure("Failed to add user to admin role", 400);
            SetupAddUserToAdminRoleCommand(userId, response);

            // Act
            var result = await _controller.AddUserToAdminRole(userId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to add user to admin role", statusCodeResult.Value);
        }

        [Fact(DisplayName = "GetCurrentUser - Success - Returns Ok with UserBase")]
        public async Task GetCurrentUser_Success_ReturnsOk()
        {
            // Arrange
            var user = UserBase;
            var response = ResponseCommon<UserBase>.Success(user);
            SetupGetCurrentUserCommand(response);

            // Act
            var result = await _controller.GetCurrentUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact(DisplayName = "GetCurrentUser - Failure - Returns Status Code 400 with error message")]
        public async Task GetCurrentUser_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<UserBase>.Failure("Failed to get current user", 400);
            SetupGetCurrentUserCommand(response);

            // Act
            var result = await _controller.GetCurrentUser();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to get current user", statusCodeResult.Value);
        }

        [Fact(DisplayName = "ListUsers - Success - Returns Ok with list of users")]
        public async Task ListUsers_Success_ReturnsOk()
        {
            // Arrange
            var users = new List<ClientUser>
            {
                ClientUser,
                ExistingClientUser
            };
            var response = ResponseCommon<List<ClientUser>>.Success(users);
            SetupListUsersCommand(response);

            // Act
            var result = await _controller.ListUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okResult.Value);
        }

        [Fact(DisplayName = "ListUsers - Failure - Returns Status Code 400 with error message")]
        public async Task ListUsers_Failure_ReturnsStatusCode()
        {
            // Arrange
            var response = ResponseCommon<List<ClientUser>>.Failure("Failed to list users", 400);
            SetupListUsersCommand(response);

            // Act
            var result = await _controller.ListUsers();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to list users", statusCodeResult.Value);
        }

        [Fact(DisplayName = "GetUserDto - Success - Returns Ok with UserDto")]
        public async Task GetUserDto_Success_ReturnsOk()
        {
            // Arrange
            int userId = 1;
            var userDto = UserDto;
            var response = ResponseCommon<UserDto>.Success(userDto);
            SetupGetUserDtoCommand(userId, response);

            // Act
            var result = await _controller.GetUserDto(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(userDto, okResult.Value);
        }

        [Fact(DisplayName = "GetUserDto - Failure - Returns Status Code 400 with error message")]
        public async Task GetUserDto_Failure_ReturnsStatusCode()
        {
            // Arrange
            int userId = 1;
            var response = ResponseCommon<UserDto>.Failure("Failed to get user", 400);
            SetupGetUserDtoCommand(userId, response);

            // Act
            var result = await _controller.GetUserDto(userId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to get user", statusCodeResult.Value);
        }
    }
}
