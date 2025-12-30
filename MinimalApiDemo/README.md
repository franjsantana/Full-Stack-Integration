# MinimalApiDemo

Esta es una aplicación de demostración construida con **.NET 9** utilizando el modelo **Minimal API**.

## Descripción

Este proyecto implementa una API REST simple para gestionar una lista de tareas (To-Do List). Utiliza una lista en memoria para el almacenamiento de datos, demostrando la simplicidad y rapidez de desarrollo que ofrecen las Minimal APIs en .NET.

### Endpoints Disponibles

La API expone los siguientes endpoints para operaciones CRUD:

- **GET** `/tasks`: Obtiene todas las tareas.
- **POST** `/tasks`: Crea una nueva tarea.
- **PUT** `/tasks/{id}`: Actualiza una tarea existente.
- **DELETE** `/tasks/{id}`: Elimina una tarea por su ID.

## Prerrequisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Cómo Ejecutar

1. Navega a la carpeta del proyecto:
   ```bash
   cd MinimalApiDemo
   ```

2. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```
   O usa el modo de recarga activa para desarrollo:
   ```bash
   dotnet watch
   ```

3. La API estará disponible en la URL indicada en la consola (ej. `http://localhost:5000`). Puedes probar los endpoints usando Postman, curl, o cualquier cliente HTTP.
