using FoodService.Models.Auth.Role;
using FoodService.Models.Auth.User;
using FoodServiceAPI.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceAPI.Config
{
    /// <summary>
    /// Class for configuring the database.
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Configures the database with the specified MySQL connection string.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="mySqlConnection">The MySQL connection string.</param>
        public static void ConfigureDatabase(this IServiceCollection services, string mySqlConnection)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            services.AddIdentity<UserBase, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Updates the database migration if there are pending migrations.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void UpdateMigrationDatabase(this IServiceCollection services)
        {
            // Configure database migration
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        dbContext.Database.Migrate();
                    }
                }
            }
        }
    }
}
