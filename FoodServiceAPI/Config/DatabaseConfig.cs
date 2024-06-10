using FoodService.Models.Auth.Role;
using FoodService.Models.Auth.User;
using FoodServiceAPI.Data.SqlServer.Config;
using FoodServiceAPI.Data.SqlServer.Context;
using Microsoft.AspNetCore.Identity;

namespace FoodServiceAPI.Config
{
    /// <summary>
    /// Class for configuring the database.
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Configures the database with the specified SQLServer connection string.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="sqlConnection">The SQLServer connection string.</param>
        public static void ConfigureDatabase(this IServiceCollection services, string sqlConnection)
        {
            services.ConfigureDatabaseSqlServer(sqlConnection);

            services.AddIdentity<UserBase, ApplicationRole>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();
        }
    }
}
