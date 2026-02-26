using AlertService.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra o nosso "Motor" para rodar em segundo plano consumindo a fila
builder.Services.AddHostedService<SensorDataConsumer>();

var app = builder.Build();

app.Run();