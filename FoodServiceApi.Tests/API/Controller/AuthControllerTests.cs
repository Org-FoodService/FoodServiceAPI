using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodService.Models.Responses;
using FoodServiceAPI.Controllers;
using FoodServiceAPI.Core.Command.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodServiceApi.Tests.API.Controller
{
    public class AuthControllerTests
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

        [Fact(DisplayName = "SignUp - Success - Returns Ok with true value")]
        public async Task SignUp_Success_ReturnsOk()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "testuser", Password = "Password123!",ConfirmPassword= "Password123!", PhoneNumber = "11911112222", Email = "testuser@example.com", CpfCnpj = "12345678901" };
            var response = ResponseCommon<bool>.Success(true);
            _mockAuthCommand.Setup(x => x.SignUp(signUpDto)).ReturnsAsync(response);

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
            var signUpDto = new SignUpDto { Username = "testuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "testuser@example.com", CpfCnpj = "12345678901" };
            var response = ResponseCommon<bool>.Failure("Sign up failed", 400);
            _mockAuthCommand.Setup(x => x.SignUp(signUpDto)).ReturnsAsync(response);

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
            var signInDto = new SignInDto { Username = "testuser", Password = "Password123!" };
            var ssoDto = new SsoDto("token", DateTime.Now.AddHours(1), new List<string> { "User" });
            var response = ResponseCommon<SsoDto>.Success(ssoDto);
            _mockAuthCommand.Setup(x => x.SignIn(signInDto)).ReturnsAsync(response);

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
            var signInDto = new SignInDto { Username = "testuser", Password = "Password123!" };
            var response = ResponseCommon<SsoDto>.Failure("Sign in failed", 401);
            _mockAuthCommand.Setup(x => x.SignIn(signInDto)).ReturnsAsync(response);

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
            _mockAuthCommand.Setup(x => x.AddUserToAdminRole(userId)).ReturnsAsync(response);

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
            _mockAuthCommand.Setup(x => x.AddUserToAdminRole(userId)).ReturnsAsync(response);

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
            var user = new UserBase { UserName = "testuser", Email = "testuser@example.com", CpfCnpj = "12345678901" };
            var response = ResponseCommon<UserBase>.Success(user);
            _mockAuthCommand.Setup(x => x.GetCurrentUser()).ReturnsAsync(response);

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
            _mockAuthCommand.Setup(x => x.GetCurrentUser()).ReturnsAsync(response);

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
                new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" },
                new ClientUser { UserName = "user2", Email = "user2@example.com", CpfCnpj = "11111111111" }
            };
            var response = ResponseCommon<List<ClientUser>>.Success(users);
            _mockAuthCommand.Setup(x => x.ListUsers()).ReturnsAsync(response);

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
            _mockAuthCommand.Setup(x => x.ListUsers()).ReturnsAsync(response);

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
            var userDto = new UserDto { UserName = "testuser", Email = "testuser@example.com" };
            var response = ResponseCommon<UserDto>.Success(userDto);
            _mockAuthCommand.Setup(x => x.GetUserDto(userId)).ReturnsAsync(response);

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
            _mockAuthCommand.Setup(x => x.GetUserDto(userId)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetUserDto(userId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
            Assert.Equal("Failed to get user", statusCodeResult.Value);
        }
    }
}