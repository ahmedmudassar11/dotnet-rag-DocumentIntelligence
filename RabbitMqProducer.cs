using RabbitMQ.Client;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace WebApplication1
{
    public class RabbitMqProducer
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqProducer()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public async Task PublishAsync<T>(T message)
        {
            Console.WriteLine("1. Starting");

            await using var connection = await _factory.CreateConnectionAsync();
            Console.WriteLine("2. Connected");

            Console.WriteLine(connection.IsOpen);

            await using var channel = await connection.CreateChannelAsync();
            Console.WriteLine("3. Channel Created");

            await channel.QueueDeclareAsync(
                queue: "user-registration",
                durable: true,
                exclusive: false,
                autoDelete: false);

            Console.WriteLine("4. Queue Declared");

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "user-registration",
                body: body);

            Console.WriteLine("5. Published");
        }
    }
}
