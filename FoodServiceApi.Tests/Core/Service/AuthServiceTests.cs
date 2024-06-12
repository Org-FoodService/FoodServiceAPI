using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodServiceApi.Tests.Utility;
using FoodServiceAPI.Core.Service;
using FoodServiceAPI.Core.Wrapper.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
            _mockUserManager = new Mock<IUserManagerWrapper<UserBase>>(store.Object, null, null, null, null, null, null, null, null);
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



        private void SetupUserManagerWithUser(ClientUser user, string role = "User")
        {
            _mockUserManager.Setup(um => um.FindByNameAsync(user.UserName!)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.FindByEmailAsync(user.Email!)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);
            _mockUserManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string> { role });
        }

        private void SetupUserManagerWithIdentityResult(IdentityResult identityResult)
        {
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<UserBase>(), It.IsAny<string>())).ReturnsAsync(identityResult);
            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<UserBase>(), "Admin")).ReturnsAsync(identityResult);
            //_mockUserManager.Setup(um => um.Users.CountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
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
    }
}

