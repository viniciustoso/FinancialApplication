using FinancialApplication.Queues.Models;
using FinancialApplication.Queues.Persistences.SqlServer;
using FinancialApplication.Utility.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace FinancialApplication.Queues.Consumers
{
    public abstract class BasicConsumer : BackgroundService
    {
        private readonly RabbitMqConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private string QueueName;

        public BasicConsumer(string queueName, IOptions<RabbitMqConfiguration> option)
        {
            QueueName = queueName;

            _configuration = option.Value;

            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                        queue: QueueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<MessageInputModel>(contentString);

                try
                {
                    await DoWork(message);
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
                catch
                {
                    _channel.BasicNack(eventArgs.DeliveryTag, false, true);
                }

            };

            _channel.BasicConsume(QueueName, false, consumer);

            return Task.CompletedTask;
        }

        protected abstract Task DoWork(MessageInputModel message);
    }
}
