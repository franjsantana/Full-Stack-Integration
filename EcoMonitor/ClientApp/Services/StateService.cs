using Blazored.LocalStorage;

namespace ClientApp.Services;

/// <summary>
/// Gestiona el estado local de la aplicación cliente (ej: dispositivos favoritos).
/// Utiliza LocalStorage para persistir los datos entre sesiones.
/// </summary>
public class StateService(ILocalStorageService localStorage)
{
    private readonly ILocalStorageService _localStorage = localStorage;
    private List<int> _favoriteDeviceIds = new();

    /// <summary> Evento que se dispara cuando el estado cambia. </summary>
    public event Action? OnChange;

    /// <summary> Lista de IDs de dispositivos marcados como favoritos. </summary>
    public IReadOnlyList<int> FavoriteDeviceIds => _favoriteDeviceIds.AsReadOnly();

    /// <summary> Carga los favoritos guardados desde el almacenamiento local. </summary>
    public async Task InitializeAsync()
    {
        _favoriteDeviceIds = await _localStorage.GetItemAsync<List<int>>("favorites") ?? new List<int>();
        NotifyStateChanged();
    }

    /// <summary> Alterna el estado de favorito de un dispositivo. </summary>
    public async Task ToggleFavoriteAsync(int deviceId)
    {
        if (_favoriteDeviceIds.Contains(deviceId))
        {
            _favoriteDeviceIds.Remove(deviceId);
        }
        else
        {
            _favoriteDeviceIds.Add(deviceId);
        }

        await _localStorage.SetItemAsync("favorites", _favoriteDeviceIds);
        NotifyStateChanged();
    }

    /// <summary> Comprueba si un dispositivo es favorito. </summary>
    public bool IsFavorite(int deviceId) => _favoriteDeviceIds.Contains(deviceId);

    private void NotifyStateChanged() => OnChange?.Invoke();
}
