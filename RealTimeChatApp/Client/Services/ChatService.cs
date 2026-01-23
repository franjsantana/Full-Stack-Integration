using Microsoft.AspNetCore.SignalR.Client;
using Shared.Models;

namespace Client.Services;

/// <summary>
/// Servicio encargado de gestionar la conexión de SignalR y la comunicación del chat en el cliente.
/// </summary>
public class ChatService
{
    // Objeto que maneja la conexión subyacente con el Hub de SignalR
    private HubConnection _hubConnection;

    /// <summary>
    /// Evento que se dispara cuando se recibe un nuevo mensaje desde el servidor.
    /// Los componentes UI pueden suscribirse a este evento para actualizar la interfaz.
    /// </summary>
    public event Action<ChatMessage>? OnMessageReceived;

    /// <summary>
    /// Constructor: Inicializa la conexión y configura los manejadores de eventos.
    /// </summary>
    public ChatService()
    {
        // Configura la conexión al Hub especificando la URL y habilitando la reconexión automática
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5272/chathub") // NOTA: Verifica que este puerto coincida con el de tu servidor
            .WithAutomaticReconnect()
            .Build();

        // Registra un manejador para el método "ReceiveMessage" que será invocado por el servidor
        _hubConnection.On<ChatMessage>("ReceiveMessage", (message) =>
        {
            // Propaga el mensaje recibido a los suscriptores (ej. la página de Chat)
            OnMessageReceived?.Invoke(message);
        });
    }

    /// <summary>
    /// Inicia la conexión asíncrona con el servidor.
    /// </summary>
    public async Task StartAsync() => await _hubConnection.StartAsync();

    /// <summary>
    /// Envía un mensaje al Hub del servidor invocando el método "SendMessage".
    /// </summary>
    /// <param name="message">El objeto mensaje a enviar.</param>
    public async Task SendMessage(ChatMessage message) => await _hubConnection.SendAsync("SendMessage", message);
}
