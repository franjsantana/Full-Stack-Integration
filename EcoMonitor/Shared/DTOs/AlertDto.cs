namespace Shared.DTOs;

/// <summary>
/// Representa una alerta o notificación generada por el sistema.
/// </summary>
public record AlertDto
{
    /// <summary> Mensaje descriptivo de la alerta. </summary>
    public string Message { get; init; } = string.Empty;
    
    /// <summary> Nivel de severidad (Info, Warning, Critical). </summary>
    public string Severity { get; init; } = "Info";
    
    /// <summary> Fecha y hora del evento. </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    
    /// <summary> ID del dispositivo relacionado (opcional). </summary>
    public int? DeviceId { get; init; }
}
