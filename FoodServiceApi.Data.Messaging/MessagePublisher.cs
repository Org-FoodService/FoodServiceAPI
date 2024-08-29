using RabbitMQ.Client;
using System.Text;

namespace FoodServiceAPI.Data.Messaging
{

    public class MessagePublisher
    {
        private readonly IModel _channel;

        public MessagePublisher(IModel channel)
        {
            _channel = channel;
        }

        public void Publish(string routingKey, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                  routingKey: routingKey,
                                  basicProperties: null,
                                  body: body);

            Console.WriteLine($" [x] Sent '{message}' to queue '{routingKey}'");
        }
    }
}
