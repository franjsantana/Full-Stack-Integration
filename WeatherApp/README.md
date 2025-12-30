# WeatherApp

Esta es una aplicación **Blazor WebAssembly** construida con **.NET 9**.

## Descripción

Este proyecto es una aplicación de cliente (Single Page Application - SPA) que simula la obtención y visualización de datos meteorológicos. Sirve como ejemplo de cómo consumir datos JSON y renderizarlos en componentes Razor.

### Características Principales

- **Fetch Data**: Obtención de datos asíncrona desde un archivo JSON estático (`weather.json` en `wwwroot`) o API.
- **Componentes Razor**: Ejemplo de lógica de presentación y código C# integrado en `WeatherFetch.razor`.
- **Inyección de Dependencias**: Configuración básica de `HttpClient` en `Program.cs`.

## Prerrequisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Cómo Ejecutar

1. Navega a la carpeta del proyecto:
   ```bash
   cd WeatherApp
   ```

2. Ejecuta la aplicación:
   ```bash
   dotnet watch
   ```
   (Para recarga rápida durante el desarrollo)

   O:
   ```bash
   dotnet run
   ```

3. Abre el navegador en la URL indicada (usualmente `http://localhost:5000`) y navega a la sección de clima (pestaña "Fetch data" o similar).

## Estructura del Proyecto

- **Pages**:
  - `Home.razor`: Página de inicio.
  - `WeatherFetch.razor`: Componente principal que muestra la tabla de pronósticos.
- **wwwroot**: Contiene los datos de muestra (`weather.json`) y recursos estáticos.
- **Program.cs**: Punto de entrada de la aplicación WebAssembly.
