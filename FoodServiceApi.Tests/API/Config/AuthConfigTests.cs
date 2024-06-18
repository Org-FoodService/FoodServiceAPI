using FoodService.Models.Auth.Role;
using FoodServiceAPI.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace FoodServiceApi.Tests.API.Config
{
    [ExcludeFromCodeCoverage]
    public class AuthConfigTests
    {
        private readonly Mock<RoleManager<ApplicationRole>> _mockRoleManager;
        private readonly Mock<IServiceProvider> _mockServiceProvider;
        private readonly Mock<IServiceScope> _mockServiceScope;

        public AuthConfigTests()
        {
            // Create a mock of IRoleStore<ApplicationRole>
            var roleStore = new Mock<IRoleStore<ApplicationRole>>();

            // Create a RoleManager with mocked IRoleStore and required parameters
            _mockRoleManager = new Mock<RoleManager<ApplicationRole>>(
                roleStore.Object,
                new IRoleValidator<ApplicationRole>[0],
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                new Mock<ILogger<RoleManager<ApplicationRole>>>().Object
            );

            // Create a mock of IServiceProvider
            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockServiceProvider.Setup(x => x.GetService(typeof(RoleManager<ApplicationRole>)))
                                .Returns(_mockRoleManager.Object);

            // Create a mock of IServiceScope
            _mockServiceScope = new Mock<IServiceScope>();
            _mockServiceScope.Setup(x => x.ServiceProvider)
                             .Returns(_mockServiceProvider.Object);
        }

        #region Setup Methods

        private void SetupRoleExistsAsync(string role, bool returns)
        {
            _mockRoleManager.Setup(x => x.RoleExistsAsync(role))
                            .ReturnsAsync(returns);
        }

        private void SetupCreateAsync()
        {
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationRole>()))
                            .ReturnsAsync(IdentityResult.Success);
        }
        private void SetupRoleExistsAsyncThrows(string role, Exception exception)
        {
            _mockRoleManager.Setup(x => x.RoleExistsAsync(role))
                            .ThrowsAsync(exception);
        }

        private void SetupCreateAsyncThrows(Exception exception)
        {
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationRole>()))
                            .ThrowsAsync(exception);
        }

        #endregion

        [Fact(DisplayName = "ConfigureAuthentication - Configures JWT authentication")]
        public void ConfigureAuthentication_ConfiguresJWTAuthentication()
        {
            // Arrange
            var services = new ServiceCollection();
            var validIssuer = "testIssuer";
            var validAudience = "testAudience";
            var secret = "supersecretkeythatneedstobe32characterslong";

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ValidIssuer", validIssuer },
                    { "ValidAudience", validAudience },
                    { "Secret", secret }
                }!)
                .Build();

            // Act
            services.ConfigureAuthentication(configuration);
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            var authService = serviceProvider.GetRequiredService<IAuthenticationService>();
            var options = serviceProvider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get(JwtBearerDefaults.AuthenticationScheme);

            Assert.NotNull(authService);
            Assert.NotNull(options);
            Assert.True(options.SaveToken);
            Assert.False(options.RequireHttpsMetadata);
            Assert.Equal(validIssuer, options.TokenValidationParameters.ValidIssuer);
            Assert.Equal(validAudience, options.TokenValidationParameters.ValidAudience);
            Assert.Equal(Encoding.UTF8.GetBytes(secret), ((SymmetricSecurityKey)options.TokenValidationParameters.IssuerSigningKey).Key);
            Assert.True(options.TokenValidationParameters.ValidateIssuer);
            Assert.True(options.TokenValidationParameters.ValidateAudience);
            Assert.True(options.TokenValidationParameters.ValidateIssuerSigningKey);
            Assert.True(options.TokenValidationParameters.ValidateLifetime);
        }

        # region AddAdminRole

        [Fact(DisplayName = "AddAdminRole - Role does not exist - Creates role")]
        public async Task AddAdminRole_RoleDoesNotExist_CreatesRole()
        {
            // Arrange
            var role = "Admin";
            SetupRoleExistsAsync(role, false);
            SetupCreateAsync();

            // Act
            await _mockServiceScope.Object.AddAdminRole();

            // Assert
            _mockRoleManager.Verify(x => x.CreateAsync(It.Is<ApplicationRole>(r => r.Name == role)), Times.Once);
        }

        [Fact(DisplayName = "AddAdminRole - Role already exists - Does not create role")]
        public async Task AddAdminRole_RoleAlreadyExists_DoesNotCreateRole()
        {
            // Arrange
            var role = "Admin";
            SetupRoleExistsAsync(role, true);

            // Act
            await _mockServiceScope.Object.AddAdminRole();

            // Assert
            _mockRoleManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationRole>()), Times.Never);
        }

        #endregion

        #region AddEmployeeRole

        [Fact(DisplayName = "AddEmployeeRole - Role does not exist - Creates role")]
        public async Task AddEmployeeRole_RoleDoesNotExist_CreatesRole()
        {
            // Arrange
            var role = "Employee";
            SetupRoleExistsAsync(role, false);
            SetupCreateAsync();

            // Act
            await _mockServiceScope.Object.AddEmployeeRole();

            // Assert
            _mockRoleManager.Verify(x => x.CreateAsync(It.Is<ApplicationRole>(r => r.Name == "Employee")), Times.Once);
        }

        [Fact(DisplayName = "AddEmployeeRole - Role already exists - Does not create role")]
        public async Task AddEmployeeRole_RoleAlreadyExists_DoesNotCreateRole()
        {
            // Arrange
            var role = "Employee";
            SetupRoleExistsAsync(role, true);

            // Act
            await _mockServiceScope.Object.AddEmployeeRole();

            // Assert
            _mockRoleManager.Verify(x => x.CreateAsync(It.IsAny<ApplicationRole>()), Times.Never);
        }

        #endregion
    }
}
