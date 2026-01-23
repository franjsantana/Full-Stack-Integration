# RealTimeChatApp

**RealTimeChatApp** es una aplicación de chat en tiempo real desarrollada utilizando **Blazor WebAssembly** para el frontend y **ASP.NET Core** con **SignalR** para el backend. Esta arquitectura permite una comunicación instantánea y fluida entre múltiples clientes.

## 🚀 Características

- **Comunicación en Tiempo Real**: Utiliza SignalR para enviar y recibir mensajes instantáneamente sin necesidad de recargar la página.
- **Arquitectura Full-Stack**: Solución completa con cliente (Blazor WASM) y servidor (ASP.NET Core API) integrados.
- **Modelos Compartidos**: Uso de una biblioteca de clases compartida (`Shared`) para asegurar la consistencia de los datos entre cliente y servidor.
- **Interfaz Sencilla**: UI limpia y funcional para enviar y visualizar mensajes con marcas de tiempo.

## 📂 Estructura del Proyecto

El proyecto está organizado en tres componentes principales:

- **Client (`RealTimeChatApp.Client`)**: 
  - Aplicación Blazor WebAssembly.
  - Contiene las páginas (como `Chat.razor`), componentes y la lógica del cliente.
  - Se conecta al `ChatHub` del servidor para enviar y recibir mensajes.

- **Server (`RealTimeChatApp.Server`)**: 
  - Aplicación ASP.NET Core que sirve la aplicación Blazor y aloja el Hub de SignalR.
  - `ChatHub.cs`: Gestiona las conexiones y la difusión de mensajes a todos los clientes conectados.

- **Shared (`RealTimeChatApp.Shared`)**: 
  - Biblioteca de clases (.NET Standard/Core) referenciada tanto por el Cliente como por el Servidor.
  - Contiene modelos de datos como `ChatMessage` para garantizar que ambos extremos hablen el mismo "idioma".

## 🛠️ Tecnologías Utilizadas

- **.NET 8** (o versión compatible del SDK)
- **Blazor WebAssembly**
- **ASP.NET Core**
- **SignalR**
- **C#**

## 🏁 Cómo Ejecutar el Proyecto

Sigue estos pasos para ejecutar la aplicación en tu máquina local:

1. **Prerrequisitos**: Asegúrate de tener instalado el [.NET SDK](https://dotnet.microsoft.com/download) adecuado.

2. **Clonar/Abrir el proyecto**: Navega hasta la carpeta raíz del proyecto.

3. **Ejecutar el Servidor**:
   Dado que es una solución *Hosted* (alojada), debes ejecutar el proyecto del **Servidor**, el cual se encargará de servir también al cliente.

   Abre una terminal en la carpeta `Server` y ejecuta:

   ```bash
   cd Server
   dotnet run
   ```
   
   O si estás en la raíz de la solución:
   
   ```bash
   dotnet run --project Server
   ```

4. **Acceder a la Aplicación**:
   La terminal mostrará las URLs en las que el servidor está escuchando (por ejemplo, `https://localhost:7123` o `http://localhost:5234`). Abre esa URL en tu navegador web.

5. **¡Chatear!**:
   - Ingresa un nombre de usuario.
   - Escribe un mensaje.
   - Presiona "Send" para enviarlo.
   - Abre la misma URL en otra pestaña o navegador para simular otro usuario y ver la comunicación en tiempo real.

## 📝 Notas Adicionales

- La lógica de comunicación principal reside en `ChatHub.cs` (Servidor) y `ChatService.cs` / `Chat.razor` (Cliente).
- El modelo de datos `ChatMessage` incluye el usuario, el mensaje y la fecha/hora de envío.
