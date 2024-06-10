using FoodServiceAPI.Data.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodServiceAPI.Data.SqlServer.Config
{
    /// <summary>
    /// Class for configuring the database.
    /// </summary>
    public static class SqlServerConfig
    {
        /// <summary>
        /// Configures the database with the specified SQLServer connection string.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="sqlConnection">The SQLServer connection string.</param>
        public static void ConfigureDatabaseSqlServer(this IServiceCollection services, string sqlConnection)
        {
            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(sqlConnection));
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
