# Full-Stack Integration Project

## 📋 Descripción General

Este es un proyecto de integración Full-Stack que demuestra la comunicación entre un **frontend Blazor WebAssembly** y un **backend ASP.NET Core Web API**. El proyecto muestra cómo consumir una API REST desde una aplicación Blazor del lado del cliente.

## 🏗️ Arquitectura del Proyecto

```
Full-Stack Integration/
├── backend/          # ASP.NET Core Web API
│   ├── Program.cs    # Configuración y endpoints de la API
│   └── ...
└── frontend/         # Blazor WebAssembly
    ├── Program.cs    # Configuración de la aplicación Blazor
    ├── Pages/
    │   └── Products.razor  # Componente que consume la API
    └── ...
```

## 🔄 Flujo de Datos

```
┌─────────────────────────────────────────────────────────────┐
│  Usuario accede a http://localhost:5269/products            │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      ▼
┌─────────────────────────────────────────────────────────────┐
│  Frontend (Blazor WebAssembly) - Puerto 5269                │
│  - Renderiza la página Products.razor                       │
│  - Ejecuta OnInitializedAsync()                             │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      │ HTTP GET Request
                      │ http://localhost:5074/products
                      ▼
┌─────────────────────────────────────────────────────────────┐
│  Backend (ASP.NET Core API) - Puerto 5074                   │
│  - Recibe la petición en el endpoint /products              │
│  - CORS permite la petición desde el frontend               │
│  - Devuelve JSON con array de productos                     │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      │ Response JSON:
                      │ [{"Id":1,"Name":"Laptop"},{"Id":2,"Name":"Phone"}]
                      ▼
┌─────────────────────────────────────────────────────────────┐
│  Frontend recibe y deserializa el JSON                      │
│  - Actualiza la variable products                           │
│  - Re-renderiza el componente                               │
│  - Muestra la lista de productos en pantalla                │
└─────────────────────────────────────────────────────────────┘
```

## 🚀 Cómo Ejecutar el Proyecto

### Prerrequisitos

- **.NET SDK 8.0 o superior** instalado
- Un navegador web moderno
- Visual Studio Code (recomendado) o Visual Studio

### Paso 1: Ejecutar el Backend

1. Abre una terminal en el directorio raíz del proyecto
2. Navega al directorio del backend:
   ```bash
   cd backend
   ```
3. Ejecuta el backend:
   ```bash
   dotnet run
   ```
4. Verifica que el backend esté corriendo. Deberías ver algo como:
   ```
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: http://localhost:5074
   ```
5. **Importante**: Anota el puerto donde corre el backend (en este caso 5074)

### Paso 2: Ejecutar el Frontend

1. Abre **otra terminal** (deja el backend corriendo)
2. Navega al directorio del frontend:
   ```bash
   cd frontend
   ```
3. Ejecuta el frontend:
   ```bash
   dotnet run
   ```
4. Verifica que el frontend esté corriendo. Deberías ver:
   ```
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: http://localhost:5269
   ```

### Paso 3: Probar la Aplicación

1. Abre tu navegador y ve a: `http://localhost:5269/products`
2. Deberías ver una lista con dos productos:
   - Laptop
   - Phone

## 🔧 Configuración Importante

### Puertos

- **Backend**: Por defecto corre en el puerto `5074`
- **Frontend**: Por defecto corre en el puerto `5269`

> ⚠️ **Nota**: Si tu backend corre en un puerto diferente, debes actualizar la URL en `frontend/Pages/Products.razor` línea 27:
> ```csharp
> products = await Http.GetFromJsonAsync<Product[]>("http://localhost:PUERTO_BACKEND/products");
> ```

### CORS (Cross-Origin Resource Sharing)

El backend está configurado para aceptar peticiones desde cualquier origen. Esto es útil para desarrollo, pero **NO recomendado para producción**.

En producción, deberías especificar los orígenes permitidos:
```csharp
policy.WithOrigins("https://tu-dominio.com")
```

## 📡 API Endpoints

### GET /products

Devuelve una lista de productos en formato JSON.

**Request:**
```http
GET http://localhost:5074/products
```

**Response:**
```json
[
  {
    "Id": 1,
    "Name": "Laptop"
  },
  {
    "Id": 2,
    "Name": "Phone"
  }
]
```

## 🧩 Componentes Principales

### Backend (`backend/Program.cs`)

- **Configuración de CORS**: Permite peticiones desde el frontend
- **Endpoint /products**: Devuelve datos de productos en formato JSON
- **Minimal API**: Usa el patrón de Minimal APIs de .NET

### Frontend (`frontend/Program.cs`)

- **Configuración de Blazor WebAssembly**: Inicializa la aplicación
- **HttpClient**: Configurado para hacer peticiones HTTP
- **Dependency Injection**: Registra servicios necesarios

### Componente Products (`frontend/Pages/Products.razor`)

- **@page "/products"**: Define la ruta de la página
- **@inject HttpClient**: Inyecta el servicio HttpClient
- **OnInitializedAsync()**: Método del ciclo de vida que se ejecuta al cargar el componente
- **Renderizado condicional**: Muestra "Loading..." mientras carga los datos

## 🛠️ Tecnologías Utilizadas

- **ASP.NET Core 8.0**: Framework para el backend
- **Blazor WebAssembly**: Framework para el frontend
- **C# 12**: Lenguaje de programación
- **Minimal APIs**: Patrón para definir endpoints de forma simple
- **HttpClient**: Cliente HTTP para consumir APIs

## 📚 Conceptos Clave

### 1. **Blazor WebAssembly**
- Ejecuta código C# directamente en el navegador usando WebAssembly
- No requiere servidor para ejecutar la lógica del frontend
- Descarga la aplicación completa al navegador la primera vez

### 2. **Minimal APIs**
- Forma simplificada de crear APIs en ASP.NET Core
- Menos código boilerplate
- Ideal para APIs pequeñas y microservicios

### 3. **Dependency Injection**
- Patrón de diseño para gestionar dependencias
- `@inject` en Blazor inyecta servicios registrados
- `HttpClient` se registra en `Program.cs` del frontend

### 4. **Ciclo de Vida de Componentes Blazor**
- `OnInitializedAsync()`: Se ejecuta cuando el componente se inicializa
- Ideal para cargar datos desde APIs
- Versión asíncrona permite usar `await`

### 5. **CORS**
- Mecanismo de seguridad del navegador
- Controla qué dominios pueden acceder a tu API
- Necesario cuando frontend y backend están en diferentes puertos/dominios

## 🔍 Solución de Problemas

### La página muestra "Loading..." indefinidamente

1. **Verifica que el backend esté corriendo**
   - Abre `http://localhost:5074/products` en tu navegador
   - Deberías ver el JSON con los productos

2. **Verifica el puerto del backend**
   - Comprueba en qué puerto corre tu backend
   - Actualiza la URL en `Products.razor` si es necesario

3. **Revisa la consola del navegador**
   - Presiona F12 para abrir las herramientas de desarrollo
   - Ve a la pestaña "Console"
   - Busca errores en rojo (CORS, conexión rechazada, etc.)

### Error de CORS

Si ves un error como:
```
Access to fetch at 'http://localhost:5074/products' from origin 'http://localhost:5269' 
has been blocked by CORS policy
```

Verifica que el backend tenga la configuración de CORS correcta en `Program.cs`.

## 📈 Próximos Pasos

Para expandir este proyecto, podrías:

1. **Agregar una base de datos** (SQL Server, PostgreSQL, SQLite)
2. **Implementar operaciones CRUD** (Create, Read, Update, Delete)
3. **Agregar autenticación y autorización**
4. **Crear más endpoints** en el backend
5. **Mejorar el diseño** del frontend con CSS/Bootstrap
6. **Agregar validación de datos**
7. **Implementar manejo de errores** más robusto

## 📝 Licencia

Este es un proyecto de demostración para fines educativos.

## 👤 Autor

Proyecto creado como ejemplo de integración Full-Stack con .NET
