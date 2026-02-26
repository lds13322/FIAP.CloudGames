using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedIntegration;

namespace AlertService.Services
{
    // Usamos BackgroundService para ele rodar continuamente em segundo plano
    public class SensorDataConsumer : BackgroundService
    {
        private readonly ILogger<SensorDataConsumer> _logger;
        private IConnection? _connection;
        private IModel? _channel;
        private readonly string _queueName = "sensor_data_queue";

        public SensorDataConsumer(ILogger<SensorDataConsumer> logger)
        {
            _logger = logger;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Garante que a fila existe antes de tentar ler
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // 1. Pega a mensagem da fila
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var sensorData = JsonSerializer.Deserialize<SensorDataMessage>(content);

                // 2. Processa a regra de negócio
                ProcessAlertLogic(sensorData);

                // 3. Avisa ao RabbitMQ que a mensagem foi processada com sucesso (tira da fila)
                _channel?.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void ProcessAlertLogic(SensorDataMessage? data)
        {
            if (data == null) return;

            _logger.LogInformation($"\n[DADOS RECEBIDOS] Talhão: {data.TalhaoId} | Temp: {data.Temperatura}°C | Umidade: {data.Umidade}%");

            // REGRA DO MVP: Gerar alerta se umidade < 30%
            if (data.Umidade < 30.0)
            {
                _logger.LogWarning($"🚨 [ALERTA DE SECA] O talhão {data.TalhaoId} está com umidade crítica ({data.Umidade}%)! Irrigação necessária.");
            }
            else
            {
                _logger.LogInformation($"✅ [STATUS NORMAL] O talhão {data.TalhaoId} está saudável.");
            }
            Console.WriteLine("--------------------------------------------------");
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}