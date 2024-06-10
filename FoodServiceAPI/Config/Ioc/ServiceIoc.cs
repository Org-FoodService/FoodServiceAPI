using FoodServiceAPI.Core.Service;
using FoodServiceAPI.Core.Service.Interface;

namespace FoodServiceAPI.Config.Ioc
{
    /// <summary>
    /// IoC configuration class for service services.
    /// </summary>
    public static class ServiceIoc
    {
        /// <summary>
        /// Configures IoC container for service services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureServiceIoc(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISiteSettingsService, SiteSettingsService>();
        }
    }
}
