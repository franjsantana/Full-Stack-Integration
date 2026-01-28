using Microsoft.AspNetCore.SignalR;
using ServerApp.Hubs;
using Shared.DTOs;

namespace ServerApp.Services;

/// <summary>
/// Servicio en segundo plano que simula la generación de datos de energía.
/// </summary>
public class TelemetrySimulator(IHubContext<NotificationHub> hubContext, ILogger<TelemetrySimulator> logger) : BackgroundService
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;
    private readonly ILogger<TelemetrySimulator> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando simulador de telemetría...");

        // Bucle principal que se ejecuta mientras el servidor esté activo
        while (!stoppingToken.IsCancellationRequested)
        {
            // 1. Simular lectura de un dispositivo aleatorio (ID 1 o 2)
            int deviceId = Random.Shared.Next(1, 3);
            decimal watts = Random.Shared.Next(100, 3000); // Rango de 100W a 3kW

            var reading = new EnergyReadingDto
            {
                DeviceId = deviceId,
                Watts = watts,
                Amps = watts / 230, // Cálculo simple basado en 230V
                Timestamp = DateTime.UtcNow,
                EstimatedCost = (watts / 1000) * 0.15m // Simulación de coste (€/kWh)
            };

            _logger.LogInformation("Enviando telemetría: Dispositivo {DeviceId} - {Watts}W", deviceId, watts);

            // 2. Transmitir los datos a todos los clientes conectados vía SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveTelemetry", reading, stoppingToken);

            // 3. Lógica de Negocio: Si el consumo es crítico (> 2500W), generar alerta
            if (watts > 2500)
            {
                var alert = new AlertDto
                {
                    Message = $"¡Consumo elevado detectado en dispositivo {deviceId}!",
                    Severity = "Critical",
                    DeviceId = deviceId,
                    Timestamp = DateTime.UtcNow
                };
                
                // Transmitir alerta vía SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveAlert", alert, stoppingToken);
            }

            // 4. Pausa de 5 segundos antes de la siguiente simulación
            await Task.Delay(5000, stoppingToken);
        }
    }
}
