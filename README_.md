# 📘 TallerFinalWebApi

## 📁 Estructura del Proyecto

```
TallerFinalWebApi/
└── TodoAppApi/
    ├── Controllers/
    │   ├── AccountController.cs
    │   ├── AdminController.cs
    │   ├── CategoriaController.cs
    │   ├── EstadoController.cs
    │   └── TareaController.cs
    ├── Models/
    │   ├── Usuario.cs
    │   ├── Categoria.cs
    │   ├── Estado.cs
    │   └── Tarea.cs
    ├── Data/
    │   └── ApplicationDbContext.cs
    └── Migrations/
```

---

## 🗃️ Diagrama ER (Entidad-Relación)

```
┌────────────┐        ┌────────────┐        ┌────────────┐
│  Usuario   │        │ Categoria  │        │  Estado    │
│────────────│        │────────────│        │────────────│
│ Id         │◄────┐  │ Id         │◄────┐  │ Id         │◄────┐
│ Nombre     │     │  │ Nombre     │     │  │ Nombre     │     │
│ Correo     │     │  │            │     │  │            │     │
└────────────┘     │  └────────────┘     │  └────────────┘     │
                   │                     │                     │
                   │                     │                     │
              ┌────▼─────────────────────▼─────────────────────▼────┐
              │                     Tarea                           │
              │─────────────────────────────────────────────────────│
              │ Id, Titulo, Descripción, FechaCreacion              │
              │ UsuarioId, CategoriaId, EstadoId (FKs)              │
              └─────────────────────────────────────────────────────┘
```

---

## 🌐 Endpoints disponibles

### 🔐 AccountController

- `POST /api/account/login`: Inicia sesión.
- `POST /api/account/register`: Registra un nuevo usuario.

### 👤 AdminController

- `GET /api/admin/usuarios`: Lista usuarios (rol Admin).
- `DELETE /api/admin/eliminar/{id}`: Elimina usuario.

### 📂 CategoriaController

- `GET /api/categoria`: Lista categorías.
- `POST /api/categoria`: Crear categoría.
- `PUT /api/categoria/{id}`: Actualizar categoría.
- `DELETE /api/categoria/{id}`: Eliminar categoría.

### 🏷️ EstadoController

- `GET /api/estado`: Lista estados.
- `POST /api/estado`: Crear estado.
- `PUT /api/estado/{id}`: Editar estado.
- `DELETE /api/estado/{id}`: Eliminar estado.

### ✅ TareaController

- `GET /api/tarea`: Lista tareas del usuario autenticado.
- `GET /api/tarea/{id}`: Consulta una tarea.
- `POST /api/tarea`: Crear tarea.
- `PUT /api/tarea/{id}`: Editar tarea.
- `DELETE /api/tarea/{id}`: Eliminar tarea.
