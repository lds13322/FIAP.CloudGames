using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FIAP.CloudGames.Pagamentos.Worker.Services; // <-- Corrigido

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddApplicationInsightsTelemetryWorkerService();
    services.AddHostedService<PagamentoListenerService>();
});

var host = builder.Build();
host.Run();