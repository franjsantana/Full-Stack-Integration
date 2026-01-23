using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;
using Shared.Models;

/// <summary>
/// Hub de SignalR que gestiona la comunicación en tiempo real del chat.
/// Hereda de la clase base Hub de ASP.NET Core SignalR.
/// </summary>
public class ChatHub : Hub
{
    /// <summary>
    /// Envía un mensaje de chat a todos los clientes conectados.
    /// </summary>
    /// <param name="chatMessage">El objeto mensaje que contiene usuario, texto y fecha.</param>
    public async Task SendMessage(ChatMessage chatMessage)
    {
        int existingVersion = GetExistingVersion(message.User);

        if (chatMessage.Version <= existingVersion)
        {
            Console.WriteLine("Version no valida");
            return;
        }

        // Asignar la marca de tiempo del servidor para consistencia
        chatMessage.TimeStamp = DateTime.UtcNow;

        // Retransmitir el mensaje a todos los clientes suscritos al evento "ReceiveMessage"
        await Clients.All.SendAsync("ReceiveMessage", chatMessage);
    }

    private int GetExistingVersion(string user)
    {
        // Implementa la lógica para obtener la versión existente del usuario
        // Por ahora, simplemente devuelve 0
        return 0;
    }
}