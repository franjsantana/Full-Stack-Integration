namespace Shared.Models;
/// <summary>
/// Modelo de datos que representa un mensaje de chat.
/// Esta clase se comparte entre el cliente y el servidor.
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// Nombre del usuario que envía el mensaje.
    /// </summary>
    public string User { get; set; }

    /// <summary>
    /// Contenido del mensaje de texto.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Fecha y hora en que se envió el mensaje.
    /// </summary>
    public DateTime TimeStamp { get; set; }
}

