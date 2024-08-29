using RabbitMQ.Client;

public class RabbitMqService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Declare a queue (creates if it does not exist)
        _channel.QueueDeclare(queue: "order_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public IModel GetChannel() => _channel;

    public void Close()
    {
        _channel.Close();
        _connection.Close();
    }
}
