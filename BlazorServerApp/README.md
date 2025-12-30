# BlazorServerApp

Esta es una aplicación **Blazor Server** construida con **.NET 9**.

## Descripción

Este proyecto demuestra el uso de componentes Blazor renderizados en el servidor con interactividad a través de SignalR. Incluye ejemplos de gestión de estado y almacenamiento en caché.

### Características Principales

- **Renderizado Interactivo (Server)**: Los componentes se ejecutan en el servidor y las actualizaciones de UI se envían al navegador vía SignalR.
- **Gestión de Sesión**: Utiliza `Blazored.SessionStorage` para manejar el almacenamiento de sesión del navegador.
- **Caché en Memoria**: Implementa `IMemoryCache` y un servicio personalizado `CacheService` para optimizar el acceso a datos.
- **Estado Distribuido**: Configurado con `AddDistributedMemoryCache` y `AddSession`.

## Prerrequisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Cómo Ejecutar

1. Navega a la carpeta del proyecto:
   ```bash
   cd BlazorServerApp
   ```

2. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```
   O para desarrollo con recarga automática:
   ```bash
   dotnet watch
   ```

3. Abre tu navegador en la dirección indicada (usualmente `http://localhost:5000`).

## Estructura del Proyecto

- **Components**: Contiene las páginas y componentes Razor de la aplicación.
- **Services**: Lógica de negocio y servicios (ej. `CacheService.cs`).
- **wwwroot**: Archivos estáticos.
- **Program.cs**: Configuración de servicios (DI) y pipeline HTTP. Incluye la configuración de `Blazored`, `Session`, y `MemoryCache`.
