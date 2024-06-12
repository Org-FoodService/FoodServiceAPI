using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodServiceAPI.Core.Command;
using FoodServiceAPI.Core.Service.Interface;
using Moq;

namespace FoodServiceApi.Tests.Core.Command
{
    public class AuthCommandTests
    {
        private readonly AuthCommand _authCommand;
        private readonly Mock<IAuthService> _mockAuthService;

        public AuthCommandTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authCommand = new AuthCommand(_mockAuthService.Object);
        }

        #region Setups
        private void SetupSignUpSuccess(SignUpDto signUpDto)
        {
            _mockAuthService.Setup(x => x.SignUp(signUpDto)).ReturnsAsync(true);
        }

        private void SetupSignUpFailure(SignUpDto signUpDto, string message)
        {
            _mockAuthService.Setup(x => x.SignUp(signUpDto)).ThrowsAsync(new ArgumentException(message));
        }

        private void SetupSignInSuccess(SignInDto signInDto, SsoDto ssoDto)
        {
            _mockAuthService.Setup(x => x.SignIn(signInDto)).ReturnsAsync(ssoDto);
        }

        private void SetupSignInFailure(SignInDto signInDto, string message)
        {
            _mockAuthService.Setup(x => x.SignIn(signInDto)).ThrowsAsync(new ArgumentException(message));
        }

        private void SetupAddUserToAdminRoleSuccess(int userId)
        {
            _mockAuthService.Setup(x => x.AddUserToAdminRole(userId)).Returns(Task.CompletedTask);
        }

        private void SetupAddUserToAdminRoleFailure(int userId, string message)
        {
            _mockAuthService.Setup(x => x.AddUserToAdminRole(userId)).ThrowsAsync(new ArgumentException(message));
        }

        private void SetupGetCurrentUserSuccess(UserBase user)
        {
            _mockAuthService.Setup(x => x.GetCurrentUser()).ReturnsAsync(user);
        }

        private void SetupGetCurrentUserFailure(string message)
        {
            _mockAuthService.Setup(x => x.GetCurrentUser()).ThrowsAsync(new ArgumentException(message));
        }

        private void SetupListUsersSuccess(List<ClientUser> users)
        {
            _mockAuthService.Setup(x => x.ListUsers()).ReturnsAsync(users);
        }

        private void SetupListUsersFailure(string message)
        {
            _mockAuthService.Setup(x => x.ListUsers()).ThrowsAsync(new ArgumentException(message));
        }

        private void SetupGetUserDtoSuccess(int userId, UserDto userDto)
        {
            _mockAuthService.Setup(x => x.GetUserDto(userId)).ReturnsAsync(userDto);
        }

        private void SetupGetUserDtoFailure(int userId, string message)
        {
            _mockAuthService.Setup(x => x.GetUserDto(userId)).ThrowsAsync(new ArgumentException(message));
        }
        #endregion

        [Fact(DisplayName = "SignUp - Success")]
        public async Task SignUp_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "testuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "testuser@example.com", CpfCnpj = "12345678901" };
            SetupSignUpSuccess(signUpDto);

            // Act
            var response = await _authCommand.SignUp(signUpDto);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact(DisplayName = "SignUp - ArgumentException Failure")]
        public async Task SignUp_ArgumentException_ReturnsFailureResponse()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "testuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "testuser@example.com", CpfCnpj = "12345678901" };
            SetupSignUpFailure(signUpDto, "Invalid input");

            // Act
            var response = await _authCommand.SignUp(signUpDto);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Invalid input", response.Message);
        }

        [Fact(DisplayName = "SignIn - Success")]
        public async Task SignIn_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var signInDto = new SignInDto { Username = "testuser", Password = "Password123!" };
            var ssoDto = new SsoDto("token", DateTime.Now.AddHours(1), new List<string> { "User" });
            SetupSignInSuccess(signInDto, ssoDto);

            // Act
            var response = await _authCommand.SignIn(signInDto);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(ssoDto, response.Data);
        }

        [Fact(DisplayName = "SignIn - ArgumentException Failure")]
        public async Task SignIn_ArgumentException_ReturnsFailureResponse()
        {
            // Arrange
            var signInDto = new SignInDto { Username = "testuser", Password = "Password123!" };
            SetupSignInFailure(signInDto, "Invalid credentials");

            // Act
            var response = await _authCommand.SignIn(signInDto);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Invalid credentials", response.Message);
        }

        [Fact(DisplayName = "AddUserToAdminRole - Success")]
        public async Task AddUserToAdminRole_Success_ReturnsSuccessResponse()
        {
            // Arrange
            int userId = 1;
            SetupAddUserToAdminRoleSuccess(userId);

            // Act
            var response = await _authCommand.AddUserToAdminRole(userId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact(DisplayName = "AddUserToAdminRole - ArgumentException Failure")]
        public async Task AddUserToAdminRole_ArgumentException_ReturnsFailureResponse()
        {
            // Arrange
            int userId = 1;
            SetupAddUserToAdminRoleFailure(userId, "User not found");

            // Act
            var response = await _authCommand.AddUserToAdminRole(userId);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("User not found", response.Message);
        }

        [Fact(DisplayName = "GetCurrentUser - Success")]
        public async Task GetCurrentUser_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var user = new UserBase { UserName = "testuser", Email = "testuser@example.com", CpfCnpj = "12345678901" };
            SetupGetCurrentUserSuccess(user);

            // Act
            var response = await _authCommand.GetCurrentUser();

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(user, response.Data);
        }

        [Fact(DisplayName = "GetCurrentUser - ArgumentException Failure")]
        public async Task GetCurrentUser_ArgumentException_ReturnsFailureResponse()
        {
            // Arrange
            SetupGetCurrentUserFailure("User not found");

            // Act
            var response = await _authCommand.GetCurrentUser();

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("User not found", response.Message);
        }

        [Fact(DisplayName = "ListUsers - Success")]
        public async Task ListUsers_Success_ReturnsSuccessResponse()
        {
            // Arrange
            var users = new List<ClientUser>
            {
                new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" },
                new ClientUser { UserName = "user2", Email = "user2@example.com", CpfCnpj = "22222222222" },
            };
            SetupListUsersSuccess(users);

            // Act
            var response = await _authCommand.ListUsers();

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(users, response.Data);
        }

        [Fact(DisplayName = "ListUsers - ArgumentException Failure")]
        public async Task ListUsers_ArgumentException_ReturnsFailureResponse()
        {
            // Arrange
            SetupListUsersFailure("Failed to list users");

            // Act
            var response = await _authCommand.ListUsers();

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Failed to list users", response.Message);
        }

        [Fact(DisplayName = "GetUserDto - Success")]
        public async Task GetUserDto_Success_ReturnsSuccessResponse()
        {
            // Arrange
            int userId = 1;
            var userDto = new UserDto { UserName = "testuser", Email = "testuser@example.com" };
            SetupGetUserDtoSuccess(userId, userDto);

            // Act
            var response = await _authCommand.GetUserDto(userId);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(userDto, response.Data);
        }

        [Fact(DisplayName = "GetUserDto - ArgumentException Failure")]
        public async Task GetUserDto_ArgumentException_ReturnsFailureResponse()
        {
            // Arrange
            int userId = 1;
            SetupGetUserDtoFailure(userId, "User not found");

            // Act
            var response = await _authCommand.GetUserDto(userId);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("User not found", response.Message);
        }
    }
}
