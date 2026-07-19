using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WebApplication1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var connection = await factory.CreateConnectionAsync();

            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "user-registration",
                durable: true,
                exclusive: false,
                autoDelete: false);

            _logger.LogInformation("Consumer Started...");

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, args) =>
            {
                var body = args.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Message Received");
                Console.WriteLine(message);
                Console.WriteLine("-----------------------------------");

                await Task.Delay(3000);

                Console.WriteLine("Processing Completed");

                await channel.BasicAckAsync(args.DeliveryTag, false);
            };

            await channel.BasicConsumeAsync(
                queue: "user-registration",
                autoAck: false,
                consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
