using FoodService.Models.Auth.Role;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodServiceAPI.Config
{
    /// <summary>
    /// Configuration class for authentication.
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// Configures authentication services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["ValidIssuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["ValidAudience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Secret"]!)),

                    ValidateLifetime = true
                };
            });
        }

        /// <summary>
        /// Adds the "Admin" role.
        /// </summary>
        /// <param name="scope">The service scope.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task AddAdminRole(this IServiceScope scope)
        {
            // Obtain the role manager service
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Check if the "Admin" role already exists
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");

            if (!adminRoleExists)
            {
                // Create a new instance of the "ApplicationRole" class
                var adminRole = new ApplicationRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                // Create the "Admin" role
                await roleManager.CreateAsync(adminRole);
            }
        }

        /// <summary>
        /// Adds the "Employee" role.
        /// </summary>
        /// <param name="scope">The service scope.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task AddEmployeeRole(this IServiceScope scope)
        {
            // Obtain the role manager service
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Check if the "Employee" role already exists
            var employeeRoleExists = await roleManager.RoleExistsAsync("Employee");

            if (!employeeRoleExists)
            {
                // Create a new instance of the "ApplicationRole" class
                var employeeRole = new ApplicationRole
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                };

                // Create the "Employee" role
                await roleManager.CreateAsync(employeeRole);
            }
        }
    }
}
