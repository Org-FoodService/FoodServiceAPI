using FoodServiceAPI.Core.Wrapper.Interface;
using FoodServiceAPI.Core.Wrapper;

namespace FoodServiceAPI.Config.Ioc
{
    public static class WrapperIoc
    {
        /// <summary>
        /// Configures IoC container for wrapper.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureWrapperIoc(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUserManagerWrapper<>), typeof(UserManagerWrapper<>));
        }
    }
}
