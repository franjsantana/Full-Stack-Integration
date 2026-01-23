# FullStackApp

Esta es una aplicación Full-Stack moderna construida con **Blazor WebAssembly** en el cliente y **.NET Minimal API** en el servidor. El proyecto demuestra la implementación de patrones de arquitectura robustos, optimización de rendimiento y buenas prácticas de integración.

## Estructura del Proyecto

- **ClientApp**: Aplicación Blazor WebAssembly que consume servicios de la API. Incluye manejo avanzado de estados de carga y errores.
- **ServerApp**: API Minimalista en .NET que gestiona datos de productos, implementa estrategias de caché multi-nivel y compresión de respuestas.

## Guía para Probar la Aplicación

Sigue estos pasos para verificar el correcto funcionamiento de `FullStackApp`:

### 1. Preparación del Entorno
Limpia y construye la solución para asegurar que todas las dependencias estén actualizadas:
```powershell
dotnet clean
dotnet build
```

### 2. Ejecutar el Servidor (ServerApp)
Navega a la carpeta del servidor y arranca la API:
```powershell
cd ServerApp
dotnet run
```
> La API estará disponible usualmente en `http://localhost:5186`. Puedes verificar el endpoint de productos en `http://localhost:5186/api/productlist`.

### 3. Ejecutar el Cliente (ClientApp)
En una nueva terminal, navega a la carpeta del cliente y arranca la aplicación:
```powershell
cd ClientApp
dotnet run
```
> La aplicación Blazor se cargará en `http://localhost:5250`.

### 4. Verificación de Comportamiento
- **Navegación**: Ve a la página "Fetch Products" en el menú lateral.
- **Carga de Datos**: Deberías ver un mensaje de "Loading products..." seguido de la lista de productos (Laptop, Headphones).
- **Caché**: Refresca la página. Notarás que la carga es casi instantánea gracias al `CacheService` implementado en el servidor.
- **Manejo de Errores**: Si detienes el servidor e intentas recargar la lista de productos, el cliente mostrará un mensaje de error amigable con un botón de "Retry".

## Asistencia de Copilot en el Desarrollo

GitHub Copilot ha sido un aliado fundamental en la construcción de esta aplicación, aportando en áreas críticas:

### 1. Generación de Código de Integración
Copilot ayudó a establecer un contrato de comunicación sólido entre el cliente y el servidor. Generó automáticamente los DTOs (Data Transfer Objects) en ambos proyectos, asegurando que los campos coincidan perfectamente y facilitando el uso de `GetFromJsonAsync` en Blazor.

### 2. Depuración y Robustez (Manejo de Errores)
Se utilizó Copilot para mejorar el manejador de eventos en `FetchProducts.razor`. Sugirió la implementación de bloques `try-catch` específicos para `HttpRequestException` y el uso de `CancellationTokenSource` para manejar tiempos de espera (timeouts), evitando que la aplicación quede colgada si el servidor no responde.

### 3. Estructuración de Respuestas JSON
Copilot asistió en la configuración global de la API para cumplir con estándares de la industria:
- Conversión automática a `camelCase`.
- Ignorar valores nulos para reducir el tamaño de la carga útil.
- Configuración de `JsonStringEnumConverter` para una mejor legibilidad de enums en el frontend.

### 4. Optimización del Rendimiento
La arquitectura de alto rendimiento fue diseñada con ayuda de Copilot:
- **CacheService**: Creación de un servicio de caché reutilizable que encapsula `IMemoryCache`.
- **Estrategia Multi-nivel**: Implementación de `ResponseCaching` y `OutputCaching` en el middleware.
- **Compresión**: Configuración de proveedores Brotli y Gzip para minimizar el ancho de banda utilizado por la API.
