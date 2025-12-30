# ClientStateApp

Esta es una aplicación **Blazor WebAssembly** construida con **.NET 9**.

## Descripción

Este proyecto demuestra cómo gestionar el estado del cliente en una aplicación Blazor WebAssembly. Utiliza librerías de terceros para persistir datos en el navegador del usuario.

### Características Principales

- **Blazored.LocalStorage**: Almacenamiento persistente que sobrevive a los reinicios del navegador.
- **Blazored.SessionStorage**: Almacenamiento de sesión que se mantiene mientras la pestaña del navegador está abierta.
- **Arquitectura Client-Side**: Todo el código se ejecuta en el navegador del cliente.

## Prerrequisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Cómo Ejecutar

1. Navega a la carpeta del proyecto:
   ```bash
   cd ClientStateApp
   ```

2. Ejecuta la aplicación:
   ```bash
   dotnet watch
   ```
   (Recomendado para desarrollo)

   O simplemente:
   ```bash
   dotnet run
   ```

3. La aplicación se abrirá en la URL indicada (usualmente `http://localhost:5000`).

## Estructura del Proyecto

- **App.razor**: Componente raíz de la aplicación.
- **Pages**: Componentes de página (`.razor`).
- **wwwroot**: Recursos estáticos (CSS, JS).
- **Program.cs**: Punto de entrada donde se registran los servicios de almacenamiento (`AddBlazoredLocalStorage`, `AddBlazoredSessionStorage`).
