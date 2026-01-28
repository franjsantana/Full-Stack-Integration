# EcoMonitor: Dashboard de Energía en Tiempo Real 🌍⚡

EcoMonitor es un proyecto full-stack desarrollado con .NET 9 y Blazor WebAssembly para demostrar la integración de múltiples tecnologías avanzadas en un único ecosistema.

## 🚀 Tecnologías Utilizadas

- **Frontend**: Blazor WebAssembly.
- **Backend**: .NET Minimal API.
- **Comunicación**: SignalR (Tiempo Real).
- **Caché**: Estrategia Multi-nivel (IMemoryCache + Output Cache).
- **Rendimiento**: Compresión de respuesta (Brotli/Gzip).
- **Estado**: Gestión de estado con persistencia en `LocalStorage`.
- **Diseño**: CSS Moderno, Animate.css y Glassmorphism.

## 🛠️ Características Principales

1.  **Dashboard Dinámico**: Visualización de vatios, amperios y coste estimado de dispositivos en tiempo real.
2.  **Sistema de Alertas**: Avisos automáticos cuando el consumo supera los 2500W.
3.  **Simulador Integrado**: Un `BackgroundService` en el servidor genera datos aleatorios para demostración.
4.  **Favoritos**: Posibilidad de marcar dispositivos para resaltarlos, con persistencia entre sesiones.
5.  **Optimización**: Uso de caché para minimizar la carga del servidor y compresión para reducir el ancho de banda.

## 💻 Cómo Ejecutar el Proyecto

1.  **Clonar el repositorio**.
2.  **Iniciar el Servidor**:
    ```bash
    cd ServerApp
    dotnet run
    ```
    *(Escuchando por defecto en http://localhost:5011)*
3.  **Iniciar el Cliente**:
    ```bash
    cd ClientApp
    dotnet run
    ```
    *(Escuchando por defecto en http://localhost:5008)*
4.  **Navegar**: Abre `http://localhost:5008` en tu navegador.

---
*Desarrollado para la carpeta de integración Full-Stack.*
