using SharedIntegration;

namespace IngestionService.Services
{
    public interface IMessageBusService
    {
        void PublishSensorData(SensorDataMessage message);
    }
}