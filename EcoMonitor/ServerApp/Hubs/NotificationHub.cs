using Microsoft.AspNetCore.SignalR;
using Shared.DTOs;

namespace ServerApp.Hubs;

/// <summary>
/// Hub de SignalR para la comunicación bidireccional en tiempo real.
/// </summary>
public class NotificationHub : Hub
{
    /// <summary>
    /// Envía lecturas de telemetría a todos los clientes.
    /// El cliente debe suscribirse al evento "ReceiveTelemetry".
    /// </summary>
    public async Task SendTelemetry(EnergyReadingDto reading)
    {
        await Clients.All.SendAsync("ReceiveTelemetry", reading);
    }

    /// <summary>
    /// Envía alertas de sistema o dispositivos a todos los clientes.
    /// El cliente debe suscribirse al evento "ReceiveAlert".
    /// </summary>
    public async Task SendAlert(AlertDto alert)
    {
        await Clients.All.SendAsync("ReceiveAlert", alert);
    }
}
