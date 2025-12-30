# AsyncApi

Esta es una aplicación **Blazor Server** construida con **.NET 9**.

## Descripción

Este proyecto demuestra el uso de componentes interactivos del lado del servidor en Blazor. Está configurado para usar `HttpClient` para realizar peticiones asíncronas.

## Prerrequisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Cómo Ejecutar

1. Navega a la carpeta del proyecto:
   ```bash
   cd AsyncApi
   ```

2. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```
   O usa el modo de observación ("watch mode") para recarga en caliente durante el desarrollo:
   ```bash
   dotnet watch
   ```

3. Abre tu navegador y ve a la URL que se muestra en la consola (usualmente `http://localhost:5000` o `https://localhost:5001`).

## Estructura del Proyecto

- **Components**: Contiene los componentes de Blazor (Páginas, Layouts, etc.).
- **wwwroot**: Archivos estáticos (CSS, JS, imágenes).
- **Program.cs**: Punto de entrada de la aplicación y configuración de servicios.
