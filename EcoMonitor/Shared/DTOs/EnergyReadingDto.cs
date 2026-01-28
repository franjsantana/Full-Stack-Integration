namespace Shared.DTOs;

/// <summary>
/// Contiene los datos de una lectura de energía en un momento específico.
/// </summary>
public record EnergyReadingDto
{
    /// <summary> ID del dispositivo que originó la lectura. </summary>
    public int DeviceId { get; init; }
    
    /// <summary> Consumo actual en Vatios (W). </summary>
    public decimal Watts { get; init; }
    
    /// <summary> Intensidad de corriente en Amperios (A). </summary>
    public decimal Amps { get; init; }
    
    /// <summary> Fecha y hora de la lectura (UTC). </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    
    /// <summary> Coste estimado basado en el consumo actual. </summary>
    public decimal EstimatedCost { get; init; }
}
