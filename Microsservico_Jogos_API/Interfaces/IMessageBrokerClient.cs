namespace WebApi.Interfaces // Verifique se este namespace está correto para seu projeto
{
    public interface IMessageBrokerClient
    {
        Task PublicarEventoAsync(string nomeFila, object evento, Dictionary<string, object> propriedades);
    }
}