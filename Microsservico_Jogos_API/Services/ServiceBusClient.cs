using System.Text.Json;
using Azure.Messaging.ServiceBus;
using WebApi.Interfaces;


namespace WebApi.Services
{
    public class ServiceBusClient : IMessageBrokerClient
    {
        private readonly IConfiguration _configuration;

        public ServiceBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task PublicarEventoAsync(string nomeFila, object evento, Dictionary<string, object> propriedades)
        {
            var connectionString = _configuration.GetConnectionString("ServiceBus");

            // --- CORREÇÃO APLICADA AQUI ---
            // Especificamos o namespace completo para desfazermos a confusão de nomes.
            await using var client = new Azure.Messaging.ServiceBus.ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(nomeFila);

            var corpoDaMensagem = JsonSerializer.Serialize(evento);
            var mensagem = new ServiceBusMessage(corpoDaMensagem);

            if (propriedades != null)
            {
                foreach (var prop in propriedades)
                {
                    mensagem.ApplicationProperties.Add(prop.Key, prop.Value);
                }
            }

            await sender.SendMessageAsync(mensagem);
        }
    }
}