namespace Shared.DTOs;

/// <summary>
/// Representa la información básica de un dispositivo en el sistema.
/// </summary>
public record DeviceDto
{
    /// <summary> Identificador único. </summary>
    public int Id { get; init; }
    
    /// <summary> Nombre descriptivo (ej: Panel Solar). </summary>
    public required string Name { get; init; }
    
    /// <summary> Categoría (Appliance, Lighting, HVAC). </summary>
    public string Type { get; init; } = "General";
    
    /// <summary> Estado de conexión/actividad. </summary>
    public bool IsActive { get; init; }
    
    /// <summary> Ubicación física (ej: Cocina, Techo). </summary>
    public string Location { get; init; } = "Main";
}
