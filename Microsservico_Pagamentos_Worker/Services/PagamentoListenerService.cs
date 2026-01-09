using System.Text.Json;
using System.Diagnostics;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting; // <-- Faltando
using Microsoft.Extensions.Logging; // <-- Faltando
using Microsoft.Extensions.Configuration; // <-- Faltando
using FIAP.CloudGames.Pagamentos.Worker.Dtos; // <-- Corrigido

namespace FIAP.CloudGames.Pagamentos.Worker.Services
{
    public class PagamentoListenerService : BackgroundService
    {
        private readonly ILogger<PagamentoListenerService> _logger;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor _processor = default!;

        public PagamentoListenerService(IConfiguration configuration, ILogger<PagamentoListenerService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionString = _configuration.GetConnectionString("ServiceBus");
            var queueName = "fila-de-compras";
            
            var client = new Azure.Messaging.ServiceBus.ServiceBusClient(connectionString);
            _processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            _logger.LogInformation("Iniciando o listener do Service Bus para a fila: {queueName}", queueName);
            await _processor.StartProcessingAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogInformation("Parando o listener do Service Bus.");
            await _processor.StopProcessingAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            
            using (var activity = new Activity("ProcessarEventoDeCompra"))
            {
                if (args.Message.ApplicationProperties.TryGetValue("traceparent", out var parentIdObject) && parentIdObject is string parentIdString)
                {
                    activity.SetParentId(parentIdString);
                }
                
                activity.Start();

                try
                {
                    var evento = JsonSerializer.Deserialize<PedidoDeCompraIniciadoDto>(body);

                    if (evento == null)
                    {
                        _logger.LogWarning("A mensagem recebida foi deserializada como nula. Conteúdo: {body}", body);
                        await args.CompleteMessageAsync(args.Message);
                        return;
                    }

                    _logger.LogInformation("Evento recebido! Processando pagamento para o usuário {UsuarioId} no valor de {Valor}", evento.UsuarioId, evento.Valor);

                    await Task.Delay(1000); 

                    await args.CompleteMessageAsync(args.Message);
                    _logger.LogInformation("Pagamento processado e mensagem removida da fila.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar a mensagem: {body}", body);
                }
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "Ocorreu um erro no listener do Service Bus.");
            return Task.CompletedTask;
        }
    }
}