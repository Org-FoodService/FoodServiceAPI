using FoodServiceAPI.Core.Interface.Repository;
using FoodServiceAPI.Core.Repository;

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
        }
    }
}
