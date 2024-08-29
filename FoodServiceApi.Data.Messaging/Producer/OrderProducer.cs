using RabbitMQ.Client;
using System.Text;

public class OrderProducer
{
    private readonly RabbitMqService _rabbitMqService;

    public OrderProducer(RabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    public void SendOrderMessage(string message)
    {
        var channel = _rabbitMqService.GetChannel();
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "order_queue",
                             basicProperties: null,
                             body: body);

        Console.WriteLine(" [x] Sent {0}", message);
    }
}

