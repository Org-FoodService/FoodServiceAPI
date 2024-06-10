using FoodServiceAPI.Data.SqlServer.Repository;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;

namespace FoodServiceAPI.Config.Ioc
{
    /// <summary>
    /// IoC configuration class for repository services.
    /// </summary>
    public static class RepositoryIoc
    {
        /// <summary>
        /// Configures IoC container for repository services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureRepositoryIoc(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ISiteSettingsRepository, SiteSettingsRepository>();
        }
    }
}
