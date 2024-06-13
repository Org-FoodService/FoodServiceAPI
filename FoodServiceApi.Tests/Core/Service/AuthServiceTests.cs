﻿using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodServiceApi.Tests.Utility;
using FoodServiceAPI.Core.Service;
using FoodServiceAPI.Core.Wrapper.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace FoodServiceApi.Tests.Core.Service
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<ILogger<AuthService>> _mockLogger;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IUserManagerWrapper<UserBase>> _mockUserManager;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public AuthServiceTests()
        {
            _mockLogger = new Mock<ILogger<AuthService>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var store = new Mock<IUserStore<UserBase>>();
            _mockUserManager = new Mock<IUserManagerWrapper<UserBase>>();
            _authService = new AuthService(
                _mockLogger.Object,
                _mockUserRepository.Object,
                _mockConfiguration.Object,
                _mockUserManager.Object,
                _mockHttpContextAccessor.Object
            );
        }

        #region Setups

        private void SetupUserRepository(List<ClientUser> users, bool userFound = false)
        {
            _mockUserRepository.Setup(repo => repo.ListAll()).Returns(new TestAsyncEnumerable<ClientUser>(users));

            if (!userFound)
                _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync((ClientUser?)null);
            else
                _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync(users.First());

            _mockUserRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ClientUser>())).ReturnsAsync(1);
            _mockUserRepository.Setup(repo => repo.DeleteAsync(It.IsAny<ClientUser>())).ReturnsAsync(true);
        }

        private void SetupUserRepositoryToThrowException()
        {
            _mockUserRepository.Setup(repo => repo.ListAll()).Throws(new Exception("Test exception"));
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
        }


        private void SetupUserManagerWithUser(ClientUser user = null, string role = "User")
        {
            if (user != null)
            {
                _mockUserManager.Setup(um => um.FindByNameAsync(user.UserName!)).ReturnsAsync(user);
                _mockUserManager.Setup(um => um.FindByEmailAsync(user.Email!)).ReturnsAsync(user);
                _mockUserManager.Setup(um => um.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
                _mockUserManager.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);
                _mockUserManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string> { role });
            }
            else
            {
                _mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ClientUser?)null);
                _mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ClientUser?)null);
            }
        }


        private void SetupUserManagerWithIdentityResult(IdentityResult identityResult)
        {
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<UserBase>(), It.IsAny<string>())).ReturnsAsync(identityResult);
            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<UserBase>(), "Admin")).ReturnsAsync(identityResult);
            _mockUserManager.Setup(um => um.CountUsersAsync()).ReturnsAsync(2);
        }


        private void SetupConfiguration()
        {
            _mockConfiguration.SetupGet(c => c["Secret"]).Returns("fedaf7d8863b48e197b9287d492b708e");
            _mockConfiguration.SetupGet(c => c["ValidIssuer"]).Returns("http://144.22.138.210:5004");
            _mockConfiguration.SetupGet(c => c["ValidAudience"]).Returns("https://144.22.138.210:5005");
        }

        private void SetupHttpContextAccessor(UserBase user)
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }, "mock"));

            _mockHttpContextAccessor.Setup(h => h.HttpContext!.User).Returns(claimsPrincipal);
            _mockUserManager.Setup(um => um.GetUserAsync(claimsPrincipal)).ReturnsAsync(user);
        }

        #endregion

        #region ListUsers
        [Fact(DisplayName = "ListUsers - Success")]
        public async Task ListUsers_Success_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<ClientUser>
            {
                new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" },
                new ClientUser { UserName = "user2", Email = "user2@example.com", CpfCnpj = "22222222222" }
            };
            SetupUserRepository(users);

            // Act
            var result = await _authService.ListUsers();

            // Assert
            Assert.Equal(users, result);
        }

        [Fact(DisplayName = "ListUsers - Error")]
        public async Task ListUsers_Error_ThrowsException()
        {
            // Arrange
            SetupUserRepositoryToThrowException();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _authService.ListUsers());

            // Verify that the exception message is the one expected
            Assert.Equal("Test exception", exception.Message);

            // Verify that the logger was called with the appropriate error message
            _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.Is<Exception>(ex => ex.Message == "Test exception"),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!),
                Times.Once
            );
        }
        #endregion

        #region GetUserById
        [Fact(DisplayName = "GetUserById - Success")]
        public async Task GetUserById_Success_ReturnsUser()
        {
            // Arrange
            var user = new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" };
            SetupUserRepository(new List<ClientUser> { user }, true);

            // Act
            var result = await _authService.GetUserById(user.Id);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact(DisplayName = "GetUserById - UserNotFound")]
        public async Task GetUserById_UserNotFound_ThrowsArgumentException()
        {
            // Arrange
            int userId = 1;
            SetupUserRepository(new List<ClientUser>());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _authService.GetUserById(userId));
        }
        #endregion

        #region GetUserDto
        [Fact(DisplayName = "GetUserDto - Success")]
        public async Task GetUserDto_Success_ReturnsUserDto()
        {
            // Arrange
            var user = new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" };
            SetupUserRepository(new List<ClientUser> { user }, true);

            // Act
            var result = await _authService.GetUserDto(user.Id);

            // Assert
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact(DisplayName = "GetUserDto - Error")]
        public async Task GetUserDto_Error_ThrowsException()
        {
            // Arrange
            var userId = 1;
            SetupUserRepositoryToThrowException();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _authService.GetUserDto(userId));

            // Verify that the exception message is the one expected
            Assert.Equal("Test exception", exception.Message);

            // Verify that the logger was called with the appropriate error message for GetUserById
            _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Error occurred while retrieving user by ID")),
                    It.Is<Exception>(ex => ex.Message == "Test exception"),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!
                ),
                Times.Once
            );

            // Verify that the logger was called with the appropriate error message for GetUserDto
            _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Error occurred while retrieving user DTO by ID")),
                    It.Is<Exception>(ex => ex.Message == "Test exception"),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!
                ),
                Times.Once
            );
        }
        #endregion


        #region UpdateUser
        [Fact(DisplayName = "UpdateUser - Success")]
        public async Task UpdateUser_Success_ReturnsAffectedRows()
        {
            // Arrange
            var user = new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" };
            SetupUserRepository(new List<ClientUser> { user }, true);

            // Act
            var result = await _authService.UpdateUser(user);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "UpdateUser - UserNotFound")]
        public async Task UpdateUser_UserNotFound_ThrowsArgumentException()
        {
            // Arrange
            var user = new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" };
            SetupUserRepository(new List<ClientUser>());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _authService.UpdateUser(user));
        }
        #endregion

        #region DeleteUser
        [Fact(DisplayName = "DeleteUser - Success")]
        public async Task DeleteUser_Success_ReturnsTrue()
        {
            // Arrange
            var user = new ClientUser { UserName = "user1", Email = "user1@example.com", CpfCnpj = "11111111111" };
            SetupUserRepository(new List<ClientUser> { user }, true);

            // Act
            var result = await _authService.DeleteUser(user.Id);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "DeleteUser - UserNotFound")]
        public async Task DeleteUser_UserNotFound_ThrowsArgumentException()
        {
            // Arrange
            int userId = 1;
            SetupUserRepository(new List<ClientUser>());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _authService.DeleteUser(userId));
        }
        #endregion

        #region SignUp
        [Fact(DisplayName = "SignUp - Success")]
        public async Task SignUp_Success_ReturnsTrue()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "newuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "newuser@example.com", CpfCnpj = "12345678901" };
            SetupUserManagerWithIdentityResult(identityResult: IdentityResult.Success);
            var user = new ClientUser { UserName = "user1", Email = "user1@example.com", Id = 1, CpfCnpj = "12345678901" };
            SetupUserManagerWithUser(user: user);

            // Act
            var result = await _authService.SignUp(signUpDto);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "SignUp - Username Already Exists")]
        public async Task SignUp_UsernameAlreadyExists_ThrowsArgumentException()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "existinguser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "newuser@example.com", CpfCnpj = "12345678901" };
            var existingUser = new ClientUser { UserName = "existinguser", Email = "existinguser@example.com", CpfCnpj = "12345678901" };
            SetupUserManagerWithUser(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authService.SignUp(signUpDto));
            Assert.Equal("Username already exists", exception.Message);
        }

        [Fact(DisplayName = "SignUp - Email Already Exists")]
        public async Task SignUp_EmailAlreadyExists_ThrowsArgumentException()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "newuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "existinguser@example.com", CpfCnpj = "12345678901" };
            var existingUser = new ClientUser { UserName = "existinguser", Email = "existinguser@example.com", CpfCnpj = "12345678901" };
            SetupUserManagerWithUser(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authService.SignUp(signUpDto));
            Assert.Equal("Email already exists", exception.Message);
        }

        [Fact(DisplayName = "SignUp - User Creation Fails")]
        public async Task SignUp_UserCreationFails_ThrowsArgumentException()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "newuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "newuser@example.com", CpfCnpj = "12345678901" };
            SetupUserManagerWithIdentityResult(IdentityResult.Failed(new IdentityError { Description = "User registration failed." }));
            SetupUserManagerWithUser();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authService.SignUp(signUpDto));
            Assert.Equal("User registration failed.", exception.Message);
        }

        [Fact(DisplayName = "SignUp - User Creation Fails Without Specific Error Messages")]
        public async Task SignUp_UserCreationFailsWithoutSpecificErrorMessages_ThrowsArgumentException()
        {
            // Arrange
            var signUpDto = new SignUpDto
            {
                Username = "newuser",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                PhoneNumber = "11911112222",
                Email = "newuser@example.com",
                CpfCnpj = "12345678901"
            };

            var failedResult = IdentityResult.Failed();
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ClientUser>(), It.IsAny<string>())).ReturnsAsync(failedResult);
            _mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ClientUser?)null);
            _mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ClientUser?)null);
            _mockUserManager.Setup(um => um.CountUsersAsync()).ReturnsAsync(2);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authService.SignUp(signUpDto));
            Assert.Equal("User registration failed.", exception.Message);
        }

        [Fact(DisplayName = "SignUp - Add to Admin Role Fails")]
        public async Task SignUp_AddToAdminRoleFails_ThrowsArgumentException()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "newuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "newuser@example.com", CpfCnpj = "12345678901" };
            SetupUserManagerWithIdentityResult(IdentityResult.Success);
            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<ClientUser>(), "Admin")).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to add user to admin role." }));
            _mockUserManager.Setup(um => um.CountUsersAsync()).ReturnsAsync(1);
            SetupUserManagerWithUser();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authService.SignUp(signUpDto));
            Assert.Equal("Failed to add user to admin role.", exception.Message);
        }

        [Fact(DisplayName = "SignUp - Add to Admin Role Success")]
        public async Task SignUp_AddToAdminRoleSuccess()
        {
            // Arrange
            var signUpDto = new SignUpDto { Username = "newuser", Password = "Password123!", ConfirmPassword = "Password123!", PhoneNumber = "11911112222", Email = "newuser@example.com", CpfCnpj = "12345678901" };
            SetupUserManagerWithIdentityResult(IdentityResult.Success);
            _mockUserManager.Setup(um => um.CountUsersAsync()).ReturnsAsync(1);
            SetupUserManagerWithUser();

            // Act
            var result = await _authService.SignUp(signUpDto);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region SignIn
        [Fact(DisplayName = "SignIn - Success")]
        public async Task SignIn_Success_ReturnsSsoDto()
        {
            // Arrange
            var signInDto = new SignInDto { Username = "user1", Password = "Password123!" };
            var user = new ClientUser { UserName = "user1", Email = "user1@example.com", Id = 1, CpfCnpj = "12345678901" };
            SetupUserManagerWithUser(user: user);
            SetupConfiguration();

            // Act
            var result = await _authService.SignIn(signInDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("user1", result.User.UserName);
        }
        #endregion

        #region GetCurrentUser
        [Fact(DisplayName = "GetCurrentUser - Success")]
        public async Task GetCurrentUser_Success_ReturnsCurrentUser()
        {
            // Arrange
            var user = new UserBase { UserName = "currentuser", Email = "currentuser@example.com", Id = 1, CpfCnpj = "12345678901" };
            SetupHttpContextAccessor(user);

            // Act
            var result = await _authService.GetCurrentUser();

            // Assert
            Assert.Equal(user, result);
        }
        #endregion

    }
}

