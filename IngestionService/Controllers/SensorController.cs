using IngestionService.Services;
using Microsoft.AspNetCore.Mvc;
using SharedIntegration;

namespace IngestionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly IMessageBusService _messageBus;

        // Injeção de Dependência aqui! Ponto para o SOLID.
        public SensorController(IMessageBusService messageBus)
        {
            _messageBus = messageBus;
        }

        [HttpPost]
        public IActionResult PostSensorData([FromBody] SensorDataMessage data)
        {
            data.DataHora = DateTime.UtcNow;
            
            // Envia para o RabbitMQ
            _messageBus.PublishSensorData(data);

            return Accepted(new { Message = "Dados recebidos e enviados para a fila com sucesso.", Data = data });
        }
    }
}