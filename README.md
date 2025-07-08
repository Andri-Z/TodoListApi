# 📝 Todo List API

Esta es una API RESTful creada para la gestion de tareas, desarrollada con ASP .NET CORE y EF Core. Permite crear, consultar, actualizar y eliminar tareas. Este proyecto esta inspirado en el [Proyecto Todo List API](https://roadmap.sh/projects/todo-list-api)

---

## ✨ Características

- Endpoints CRUD para tareas (Crear, Leer, Actualizar, Eliminar).
- Paginación en la consulta de tareas.
- Validaciones y manejo de errores.
- Actualización parcial del estado de la tarea.
- Uso de DTOs para entrada y salida de datos.
- Registro de errores mediante logging con ILogger.
- Autenticación básica (requiere token para acceder a los endpoints).
- Arquitectura limpia con separación de responsabilidades (Controllers, Services, Models, DTOs, context)

---

## 🚀 Endpoints principales

| Método | Ruta                     | Descripción                                |
|--------|--------------------------|--------------------------------------------|
| GET    | `/tasks`                 | Obtener todas las tareas                   |
| GET    | `/tasks{id}`             | Obtener una tarea por su ID                |
| POST   | `/tasks`                 | Crear una nueva tarea                      |
| PUT    | `/tasks/{id}`            | Actualizar una tarea existente             |
| PATCH  | `/tasks/{id}/status`     | Actualizar el estado de una tarea          |
| DELETE | `/tasks/{id}`            | Eliminar una tarea  por su ID              |

---

##  🧱  Modelo de datos

| Campos    | Tipo                | Descripción                                
|-------------|---------------------|--------------------------------------------------|
| id          | `int`               | Identificador de la tarea                        |
| Title       | `string`            | Titulo de la tarea                               |
| Description | `string`            | Descripcion detallada                            |
| Status      | `enum`              | Estado: `Pendiente`, `En proceso`, `Completada`  |

---

## ⚙️ Puntos importantes:
- Todos los EndPoints estan protegidos por `[Authorize]`.
- Para obtener todas las tareas, es fundamental ingresar los parametros `page` y `limit`.
- Si ocurre un error inesperado se mostrara un mensaje personalizado.
- En el archivo appsettings.json se encuentra la configuracion de JWT, los campos Issuer, Audience y Key estaran vacios, por lo que hay que rellenarlos con la informacion correspondiente:
- 
```json
"JwtConfig": {
  "Issuer": "",
  "Audience": "",
  "Key": "",
  "TokenValidityMins": 30
}
```
