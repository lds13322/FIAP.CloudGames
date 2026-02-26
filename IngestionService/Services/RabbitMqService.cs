using RabbitMQ.Client;
using SharedIntegration;
using System.Text;
using System.Text.Json;

namespace IngestionService.Services
{
    public class RabbitMqService : IMessageBusService
    {
        private readonly string _hostname = "localhost"; // O Docker expõe o RabbitMQ no localhost
        private readonly string _queueName = "sensor_data_queue";

        public void PublishSensorData(SensorDataMessage message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var messageJson = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}