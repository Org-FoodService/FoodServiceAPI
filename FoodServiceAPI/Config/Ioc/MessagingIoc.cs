using FoodServiceAPI.Data.Messaging;

namespace FoodServiceAPI.Config.Ioc
{
    public static class MessagingIoc
    {
        /// <summary>
        /// Configures IoC container for messaging services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureMessagingIoc(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMqService>();
            services.AddSingleton(sp =>
            {
                var rabbitMqService = sp.GetRequiredService<RabbitMqService>();
                return new MessagePublisher(rabbitMqService.GetChannel());
            });
        }
    }
}
