using FoodServiceAPI.Core.Service.Interface;
using FoodServiceAPI.Core.Service;

namespace FoodServiceAPI.Config.Ioc
{
    public static class WrapperIoc
    {
        /// <summary>
        /// Configures IoC container for wrapper.
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
