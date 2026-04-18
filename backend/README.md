# 🖥️ Backend — TalentInsights

API REST para la plataforma **TalentInsights**: un sistema de reconocimiento y seguimiento del crecimiento de colaboradores dentro de una empresa. Construida con **ASP.NET Core 9**, **Entity Framework Core**, **JWT**, **caché en memoria**, **SMTP** y **Serilog**, siguiendo una arquitectura limpia en 4 capas.

---

## 📁 Estructura del proyecto

```
backend/TalentInsights/
├── TalentInsights.Application/       # Lógica de negocio, servicios, modelos, helpers
├── TalentInsights.Domain/            # Entidades, contexto EF, interfaces, excepciones
├── TalentInsights.Infrastructure/    # Implementaciones de repositorios y Unit of Work
├── TalentInsights.Shared/            # Utilidades, constantes y helpers transversales
├── TalentInsights.WebApi/            # Controladores, middlewares, DI, Program.cs
├── TalentInsights.slnx               # Archivo de solución
├── api.postman_collection.json       # Colección Postman (versión simple)
└── utils/
    └── api.postman_collection.json   # Colección Postman (versión extendida)
```

---

## 🏗️ Arquitectura de capas

```
WebApi  ──►  Application  ──►  Domain
  │                │               ▲
  └──►  Shared ◄───┘               │
              │                    │
              └────────────────────┘
Infrastructure ──► Domain
```

- **WebApi** depende de Application, Domain, Infrastructure y Shared.
- **Application** depende de Domain y Shared.
- **Infrastructure** depende de Domain.
- **Domain** no depende de ninguna capa interna.
- **Shared** no depende de ninguna capa.

---

## 📦 Dependencias NuGet por proyecto

### `TalentInsights.WebApi`
- `Microsoft.AspNetCore.Authentication.JwtBearer` — autenticación JWT Bearer
- `Microsoft.AspNetCore.OpenApi` — documentación OpenAPI
- `Serilog.AspNetCore` — integración Serilog con ASP.NET Core
- `Serilog.Sinks.File` — logs diarios en archivo

### `TalentInsights.Application`
- `Microsoft.EntityFrameworkCore` — acceso base EF Core
- `Microsoft.Extensions.Caching.Memory` — caché en memoria
- `Microsoft.IdentityModel.Tokens` + `System.IdentityModel.Tokens.Jwt` — JWT

### `TalentInsights.Domain`
- `Microsoft.EntityFrameworkCore.SqlServer` — proveedor SQL Server

### `TalentInsights.Infrastructure`
- `Microsoft.EntityFrameworkCore` — acceso base EF Core

### `TalentInsights.Shared`
- Sin dependencias NuGet externas

---

## ⚙️ Configuración (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "Database": "Server=...;Database=TalentInsights;..."
  },
  "TokenConfiguration": {
    "Issuer": "TalentInsights",
    "Audience": "TalentInsights",
    "SecretKey": "...",
    "AccessTokenExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  },
  "SMTP": {
    "Host": "smtp.example.com",
    "From": "noreply@example.com",
    "Port": 587,
    "User": "user@example.com",
    "Password": "..."
  },
  "FirstAppTimeUser": {
    "FullName": "...",
    "Email": "...",
    "Position": "...",
    "Password": "..."
  }
}
```

> La cadena de conexión también puede provenir de la variable de entorno definida en `ConfigurationConstants.CONNECTION_STRING_DATABASE`, que tiene prioridad sobre `appsettings.json`.

---

## 🗂️ Capa: `TalentInsights.Shared`

Utilidades y constantes transversales. No depende de ninguna capa interna.

### `Hasher.cs`
Hashing de contraseñas con **PBKDF2 + SHA-256**:
- `HashPassword(string password)` — genera un hash salteado.
- `VerifyPassword(string password, string hash)` — verifica la contraseña contra su hash.

### `Generate.cs`
- `RandomText(int length)` — genera una cadena aleatoria de longitud dada, usada para crear contraseñas temporales al registrar colaboradores.

### `SMTP.cs`
Clase singleton que encapsula la configuración y el envío de correos electrónicos vía SMTP:
- Constructor: recibe `host`, `from`, `port`, `user`, `password`.
- `Send(string to, string subject, string body)` — envía un correo HTML. Se registra como `Singleton` en el contenedor de DI.

### `Cache.cs`
Define constantes de claves de caché usadas en toda la aplicación.

### `Helpers/DateTimeHelper.cs`
- `UtcNow()` — devuelve `DateTime.UtcNow`, centralizando el acceso a la hora UTC.

### `Constants/ClaimsConstants.cs`
Claves de los claims JWT usados en los tokens de acceso.

### `Constants/ConfigurationConstants.cs`
Claves de las secciones de `appsettings.json` y variables de entorno:
- `CONNECTION_STRING_DATABASE` — cadena de conexión a SQL Server.
- `SMTP_HOST`, `SMTP_FROM`, `SMTP_PORT`, `SMTP_USER`, `SMTP_PASSWORD` — configuración SMTP.
- `FIRST_APP_TIME_USER_FULLNAME`, `FIRST_APP_TIME_USER_EMAIL`, `FIRST_APP_TIME_USER_POSITION`, `FIRST_APP_TIME_USER_PASSWORD` — datos del primer colaborador Admin creado al iniciar.

### `Constants/ResponseConstants.cs`
Mensajes de respuesta estandarizados, incluyendo:
- Mensajes de error para colaboradores, auth, roles, configuración.
- `ConfigurationPropertyNotFound(string key)` — genera un mensaje de error para propiedades faltantes en `appsettings.json`.
- `RoleNotFound(...)` — mensajes para roles inexistentes.
- `CANNOT_ASSIGN_THE_ROLE` — cuando un HR intenta asignar el rol Admin.

### `Constants/RoleConstants.cs`
Nombres de los roles del sistema: `Admin`, `HR`, `TeamLeader`, `Developer`.

### `Constants/ValidationConstants.cs`
Mensajes de validación de modelos de request, longitudes mínimas/máximas, y el método `IsEmpty(string field)`.

### `Constants/EmailTemplateNameConstants.cs`
Nombres de las plantillas de email almacenadas en base de datos:
- `COLLABORATOR_REGISTER` — enviado al crear un colaborador.
- `AUTH_LOGIN_SUCCESS` — enviado al iniciar sesión exitosamente.
- `AUTH_LOGIN_FAILED` — enviado cuando falla el login.

---

## 🗂️ Capa: `TalentInsights.Domain`

Define el núcleo del negocio. No depende de ninguna capa de la aplicación.

### `Database/SqlServer/Context/TalentInsightsContext.cs`
Contexto de Entity Framework Core que mapea las entidades al esquema SQL Server. Incluye configuración de relaciones, constraints, índices únicos y valores por defecto generados mediante `OnModelCreating`.

**DbSets expuestos:** `Collaborators`, `CollaboratorHistories`, `CollaboratorPermissions`, `CollaboratorRoles`, `CollaboratorSkills`, `EmailTemplates`, `Menus`, `MenuPermissions`, `Permissions`, `Posts`, `Projects`, `ProjectCollaborators`, `ProjectMessages`, `Roles`, `RolePermissions`, `Skills`, `Teams`, `TeamMembers`, `Users`, `UsersRoles`.

### `Database/SqlServer/IUnitOfWork.cs`
Contrato del patrón Unit of Work. Expone los repositorios disponibles y el método de guardado:
```csharp
public interface IUnitOfWork
{
    ICollaboratorRepository collaboratorRepository { get; }
    IEmailTemplateRepository emailTemplateRepository { get; }
    IRoleRepository roleRepository { get; }
    Task SaveChangesAsync();
}
```

### `Database/SqlServer/Entities/`
Entidades C# que mapean las tablas de SQL Server:

| Entidad | Tabla | Descripción |
|---|---|---|
| `Collaborator` | `Collaborators` | Colaborador principal. Navega a: CollaboratorRoles, Skills, History, Posts, ProjectMessages |
| `CollaboratorRole` | `CollaboratorRoles` | Asignación de rol. Navega a: Collaborator, Role, AssignedBy (auto-ref) |
| `CollaboratorSkill` | `CollaboratorSkills` | Habilidad adquirida por un colaborador |
| `CollaboratorHistory` | `CollaboratorHistory` | Registro histórico de participación |
| `CollaboratorPermission` | `CollaboratorPermissions` | Permisos directos asignados a un colaborador |
| `Role` | `Roles` | Rol del sistema. Navega a: RolePermissions, CollaboratorRoles |
| `Permission` | `Permissions` | Permiso atómico del sistema con Code, Module, Action y Specificity |
| `RolePermission` | `RolePermissions` | N:M entre roles y permisos |
| `Menu` | `Menus` | Ítem de navegación con jerarquía padre-hijo |
| `MenuPermission` | `MenuPermissions` | Permisos necesarios para ver un menú, con MatchMode (ANY/ALL) |
| `Skill` | `Skills` | Habilidad técnica o blanda |
| `Team` | `Teams` | Equipo de trabajo |
| `TeamMember` | `TeamMembers` | Miembro de un equipo |
| `Project` | `Projects` | Proyecto de la empresa |
| `ProjectCollaborator` | `ProjectCollaborators` | Colaboradores asignados a proyectos |
| `ProjectMessage` | `ProjectMessages` | Mensajes dentro de un proyecto |
| `Post` | `Posts` | Publicación de un colaborador |
| `EmailTemplate` | `EmailTemplates` | Plantilla de correo con variables interpolables |
| `User` | `Users` | (entidad auxiliar en el contexto) |
| `UsersRole` | `UsersRoles` | (entidad auxiliar en el contexto) |

### `Domain/Exceptions/`
Excepciones de dominio capturadas por el middleware:

| Clase | HTTP Status |
|---|---|
| `BadRequestException` | 400 |
| `NotFoundException` | 404 |
| `UnauthorizedException` | 401 |

### `Domain/Interfaces/Repositories/`

#### `IGenericRepository<T>`
Repositorio genérico con operaciones base:
- `Get(Guid id)` — por ID.
- `Get(Expression<Func<T, bool>> predicate)` — por expresión lambda.
- `IfExists(Expression<Func<T, bool>> predicate)` — verifica existencia.
- `Queryable()` — devuelve `IQueryable<T>` para consultas LINQ adicionales.

#### `ICollaboratorRepository`
Extiende `IGenericRepository<Collaborator>` con:
- `Create(Collaborator collaborator)` — crea un colaborador.
- `Update(Collaborator collaborator)` — actualiza un colaborador.
- `HasCreated()` — verifica si ya existe al menos un colaborador (usado en la inicialización).
- `ClearRoles(IEnumerable<CollaboratorRole> roles)` — elimina los roles actuales de un colaborador antes de asignar uno nuevo.

#### `IEmailTemplateRepository`
- `GetByName(string name)` — obtiene una plantilla por su nombre identificador.

#### `IRoleRepository`
- Hereda `IGenericRepository<Role>`.
- `Get(Guid id)` — obtiene un rol por ID.
- `Get(Expression<Func<Role, bool>> predicate)` — búsqueda por lambda.

#### `ITeamRepository`
- Contrato base para equipos (en expansión).

---

## 🗂️ Capa: `TalentInsights.Infrastructure`

Implementaciones concretas de los contratos de `Domain`. Solo depende de `Domain`.

### `Persistence/SqlServer/UnitOfWork.cs`
Implementa `IUnitOfWork`. Inyecta `TalentInsightsContext` y expone los tres repositorios como propiedades. `SaveChangesAsync()` delega en el contexto EF.

### `Persistence/SqlServer/Repositories/GenericRepository<T>`
Implementación base de `IGenericRepository<T>`. Usa `DbContext.Set<T>()` y LINQ para todas las operaciones. Es la clase base de la que heredan los repositorios especializados.

### `Persistence/SqlServer/Repositories/CollaboratorRepository.cs`
Implementa `ICollaboratorRepository`. Extiende `GenericRepository<Collaborator>`:
- `Create` — agrega al contexto con sus `CollaboratorRoles` relacionados.
- `Update` — marca como modificado.
- `HasCreated` — `AnyAsync()` sobre la tabla.
- `ClearRoles` — elimina en rango los roles del colaborador del contexto.

### `Persistence/SqlServer/Repositories/EmailTemplateRepository.cs`
Implementa `IEmailTemplateRepository`. Busca por `Name` con `FirstOrDefaultAsync`.

### `Persistence/SqlServer/Repositories/RoleRepository.cs`
Implementa `IRoleRepository`. Usa `FindAsync` para búsqueda por ID y `FirstOrDefaultAsync` con lambda.

---

## 🗂️ Capa: `TalentInsights.Application`

Lógica de negocio, contratos de servicios, modelos de datos y helpers.

### `Interfaces/Services/`

#### `ICollaboratorService`
Contrato del servicio principal de colaboradores:
- `Create(CreateCollaboratorRequest model, Claim claim)` — crea un colaborador con contraseña aleatoria, asigna rol y envía email.
- `Update(Guid id, UpdateCollaboratorRequest model, Claim claim)` — actualiza datos y/o rol.
- `Delete(Guid id)` — soft delete (rellena `DeletedAt`).
- `Get(FilterColaboratorRequest model)` — lista con filtros, paginación y joins.
- `Get(Guid id)` — obtiene uno por ID.
- `Me(Claim claim)` — devuelve el colaborador autenticado.
- `CreateFirstUser()` — crea el primer Admin al iniciar la app si no existe ninguno.

#### `IAuthService`
- `Login(LoginAuthRequest request)` — autentica, genera JWT y refresh token, envía email de notificación.
- `RenewToken(RenewAuthRequest request)` — renueva el access token vía refresh token en caché.

#### `ICacheService`
- `Get<T>(string key)`, `Set<T>(string key, T value)`, `Remove(string key)`.

#### `IEmailTemplateService`
- `Init()` — carga todas las plantillas desde base de datos al iniciar la app y las guarda en un `EmailTemplateData` singleton.
- `Get(string name, Dictionary<string, string> variables)` — obtiene una plantilla e interpola las variables `{{key}}` → `value`.

#### `IAppService`
- `GetInfo()` — devuelve información general de la aplicación (versión, nombre, etc.).

---

### `Services/`

#### `CollaboratorService.cs`
Servicio más complejo del sistema. Implementa `ICollaboratorService`.

**`Create(CreateCollaboratorRequest model, Claim claim)`**
1. Obtiene el **executor** (colaborador autenticado) desde el claim JWT.
2. Valida que `RoleId` no sea `Guid.Empty`.
3. Verifica que el email no esté ya registrado (`ValidateEmailIfExists`).
4. Genera una contraseña aleatoria de 32 caracteres con `Generate.RandomText`.
5. Valida el rol a asignar con `ValidateRole` — un HR no puede asignar el rol Admin.
6. Crea el `Collaborator` con su `CollaboratorRole` embebido.
7. Obtiene la plantilla `COLLABORATOR_REGISTER` con la contraseña interpolada.
8. Envía el correo al colaborador con `SMTP.Send`.
9. Persiste con `SaveChangesAsync`.

**`Update(Guid id, UpdateCollaboratorRequest model, Claim claim)`**
1. Obtiene el executor y el colaborador a actualizar.
2. Actualiza solo los campos no nulos del request (actualización parcial).
3. Si cambia el email, valida que no esté en uso.
4. Si se proporciona un nuevo `RoleId`, limpia los roles actuales con `ClearRoles` y asigna el nuevo.
5. Persiste y devuelve `CollaboratorDto`.

**`Delete(Guid id)`**
- Soft delete: establece `DeletedAt = DateTimeHelper.UtcNow()`.

**`Get(FilterColaboratorRequest model)`**
- Usa `IQueryable` con `Include` + `ThenInclude` para cargar los roles de cada colaborador.
- Aplica `CollaboratorFilterQuery` para filtros dinámicos.
- Aplica paginación con `.Skip(Offset).Take(Limit)`.
- Devuelve `count` total (sin paginación) junto con la lista paginada.

**`Me(Claim claim)`**
- Extrae el `UserId` del claim y devuelve el colaborador autenticado.

**`CreateFirstUser()`**
- Al inicio: si `HasCreated()` es false, lee la config de `appsettings.json` (`FirstAppTimeUser`), busca el rol Admin y crea el primer colaborador Admin.

**Métodos privados:**
- `GetCollaborator(Guid id)` — lanza `NotFoundException` si no existe.
- `GetExecutor(string value)` — parsea el claim como GUID y obtiene el colaborador.
- `ValidateEmailIfExists(string email)` — lanza `BadRequestException` si el email ya está registrado.
- `ValidateRole(Collaborator executor, Guid roleId)` — lanza `BadRequestException` si HR intenta asignar Admin.
- `Map(Collaborator collaborator)` — convierte a `CollaboratorDto` con su `RoleDto` embebido.

#### `AuthService.cs`
Implementa `IAuthService`:

**`Login(LoginAuthRequest request)`**
1. Busca el colaborador por email.
2. Verifica contraseña con `Hasher.VerifyPassword`.
3. Si la verificación falla, envía el email `AUTH_LOGIN_FAILED` y lanza `UnauthorizedException`.
4. Si es exitosa, genera access token JWT y refresh token.
5. Guarda el refresh token en caché.
6. Envía el email `AUTH_LOGIN_SUCCESS` con la fecha y hora del login.
7. Devuelve `LoginAuthResponse` con ambos tokens.

**`RenewToken(RenewAuthRequest request)`**
1. Valida el refresh token desde caché.
2. Si es válido, genera un nuevo access token.
3. Devuelve el nuevo token.

#### `CacheService.cs`
Wrapper sobre `IMemoryCache`. Implementa `ICacheService` con `Get<T>`, `Set<T>` y `Remove`.

#### `EmailTemplateService.cs`
Implementa `IEmailTemplateService`:
- **`Init()`** — al arrancar, obtiene todas las plantillas de la base de datos y las carga en el singleton `EmailTemplateData` (diccionario en memoria).
- **`Get(string name, Dictionary<string, string> variables)`** — busca la plantilla en memoria, interpola las variables (`{{key}}` → valor) en el `Body` y devuelve `EmailTemplateDto` con `Subject` y `Body` ya procesados.

#### `AppService.cs`
Implementa `IAppService`. Devuelve un `AppInfoDto` con metadatos de la aplicación leídos de configuración o del ensamblado.

---

### `Queries/CollaboratorFilterQuery.cs`
Query object que aplica filtros dinámicos a un `IQueryable<Collaborator>`:
- Filtra por `IsActive` si se proporciona.
- Filtra por nombre con `Contains` si se proporciona un término de búsqueda.
- Se usa en `CollaboratorService.Get(FilterColaboratorRequest)` mediante el método de extensión `ApplyQuery`.

---

### `Helpers/`

#### `TokenHelper.cs`
- `Configuration(IConfiguration config)` — lee `TokenConfiguration` de `appsettings.json` y construye el objeto con la `SecurityKey`.
- `GenerateAccessToken(Collaborator collaborator, TokenConfiguration config)` — genera un JWT con claims: `CollaboratorId`, `Email`. Firmado con HMAC-SHA256.
- `GenerateRefreshToken()` — genera un token seguro aleatorio.
- `ValidateRefreshToken(...)` — verifica el refresh token desde caché.

#### `CacheHelper.cs`
- `GetOrSetAsync<T>(ICacheService cache, string key, Func<Task<T>> factory)` — patrón **cache-aside**: intenta leer de caché, si no existe ejecuta la factory, guarda el resultado y lo devuelve.

#### `ResponseHelper.cs`
- `Create<T>(T data, string message, List<string> errors, int? count)` — factory de `GenericResponse<T>`.
- La sobrecarga con `count` es especialmente útil para respuestas paginadas (devuelve el total junto con la página actual).

---

### `Models/`

#### DTOs
| DTO | Campos principales |
|---|---|
| `CollaboratorDto` | `CollaboratorId`, `FullName`, `Email`, `Position`, `GitlabProfile`, `JoinedAt`, `IsActive`, `CreatedAt`, `Role` (RoleDto embebido) |
| `RoleDto` | `Id`, `Name`, `Description` |
| `EmailTemplateDto` | `Subject`, `Body` |
| `AppInfoDto` | Metadatos de la aplicación |

#### Requests — Collaborator
| Request | Campos |
|---|---|
| `CreateCollaboratorRequest` | `FullName` (requerido), `Email` (requerido), `Position` (requerido), `GitlabProfile` (opcional), `RoleId` (requerido) |
| `UpdateCollaboratorRequest` | Todos opcionales: `FullName`, `Email`, `Position`, `GitlabProfile`, `RoleId?` |
| `ChangePasswordCollaboratorRequest` | `CurrentPassword`, `NewPassword` |
| `FilterColaboratorRequest` | `IsActive?`, búsqueda por nombre, `Offset`, `Limit` (paginación) |

#### Requests — Auth
| Request | Campos |
|---|---|
| `LoginAuthRequest` | `Email` (requerido), `Password` (requerido) |
| `RenewAuthRequest` | `RefreshToken` (requerido) |

#### `BaseRequest`
Clase base para requests con paginación: `Offset` (int), `Limit` (int).

#### Responses
| Modelo | Descripción |
|---|---|
| `GenericResponse<T>` | Envuelve cualquier respuesta: `Data`, `Message`, `Errors`, `Count` (para paginación) |
| `LoginAuthResponse` | `AccessToken`, `RefreshToken` |

#### `Models/Helpers/`
- `TokenConfiguration` — configuración del JWT con `SecurityKey` derivada del secreto.
- `RefreshToken` — modelo del refresh token en caché (`Token`, `ExpiresAt`).
- `CacheKey` — encapsula claves de caché.

#### `Models/Services/EmailTemplates/EmailTemplateData.cs`
Singleton que actúa como caché en memoria de las plantillas de email. Es un `Dictionary<string, EmailTemplateDto>` cargado al iniciar la app por `EmailTemplateService.Init()`.

---

## 🗂️ Capa: `TalentInsights.WebApi`

Capa de presentación. Expone los endpoints HTTP y configura el pipeline de ASP.NET Core.

### `Program.cs`
```csharp
await builder.Services.AddCore(builder.Configuration);
var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### `Extensions/ServiceCollectionExtension.cs`
Clase estática que centraliza toda la configuración del contenedor DI:

| Método | Qué registra |
|---|---|
| `AddServices()` | `ICollaboratorService`, `IAuthService`, `ICacheService`, `IEmailTemplateService`, `IAppService` como Scoped |
| `AddRepositories()` | `IUnitOfWork`, `ICollaboratorRepository`, `IEmailTemplateRepository`, `IRoleRepository` como Scoped |
| `AddMiddlewares()` | `ErrorHandlerMiddleware` como Scoped |
| `AddLogging()` | Serilog con sinks: consola + archivo diario |
| `AddCache()` | `IMemoryCache` |
| `AddAuth(config)` | JWT Bearer con validación completa (issuer, audience, firma, lifetime); lanza `UnauthorizedException` en `OnChallenge` |
| `AddSMTP(config)` | Lee configuración SMTP y registra `SMTP` como **Singleton** |
| `Initialize()` | Carga `EmailTemplateData`, llama `CreateFirstUser()` y `EmailTemplateService.Init()` |
| `AddCore(config)` | Orquesta todos los anteriores: SMTP → Controllers → OpenAPI → SqlServer → Repositories → Services → Middlewares → Logging → Auth → Cache → Initialize |

**Diferencia clave respecto al proyecto anterior:** `AddSMTP` valida activamente que todas las propiedades de configuración estén presentes en `appsettings.json` al iniciar; si falta alguna, lanza una excepción con un mensaje descriptivo usando `ConfigurationPropertyNotFound`.

### `Controllers/`

#### `AppController.cs`
Ruta base: `api/app`

| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| `GET` | `api/app` | ❌ Público | Devuelve información de la aplicación |

#### `AuthController.cs`
Ruta base: `api/auth`

| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| `POST` | `api/auth/login` | ❌ Público | Login con email/password, devuelve access + refresh token |
| `POST` | `api/auth/renew` | ❌ Público | Renueva el access token con refresh token |

#### `CollaboratorsController.cs`
Ruta base: `api/collaborators`

| Método | Ruta | Auth | Descripción |
|---|---|---|---|
| `GET` | `api/collaborators` | ✅ JWT | Lista colaboradores con filtros y paginación |
| `GET` | `api/collaborators/{id}` | ✅ JWT | Obtiene un colaborador por ID |
| `GET` | `api/collaborators/me` | ✅ JWT | Devuelve el colaborador autenticado |
| `POST` | `api/collaborators` | ✅ JWT | Crea un colaborador y le envía su contraseña por email |
| `PUT` | `api/collaborators/{id}` | ✅ JWT | Actualiza datos y/o rol de un colaborador |
| `DELETE` | `api/collaborators/{id}` | ✅ JWT | Soft delete de un colaborador |

> Todos los endpoints protegidos extraen el claim del JWT usando `User.FindFirst(ClaimsConstants.CollaboratorId)` y lo pasan al servicio para validar permisos del executor.

### `Attributes/DeveloperAuthor.cs`
Atributo personalizado de documentación/autoría. Marca endpoints o controladores con metadata del desarrollador que los implementó.

### `Helpers/ResponseStatus.cs`
Helper que define constantes de nombres de estado HTTP usados en las respuestas del middleware.

### `Middlewares/ErrorHandlerMiddleware.cs`
Middleware de manejo de errores centralizado. Intercepta excepciones y devuelve JSON estandarizado:

| Excepción | HTTP Status |
|---|---|
| `BadRequestException` | 400 |
| `NotFoundException` | 404 |
| `UnauthorizedException` | 401 |
| Cualquier otra | 500 |

Registra cada error con `Serilog` incluyendo el stack trace.

---

## 🔐 Flujo de autenticación y notificaciones

```
Cliente          WebApi           Application         Shared / SMTP
  │                │                  │                    │
  │── POST /auth/login ──►            │                    │
  │                │── Login() ──►    │                    │
  │                │                  │── VerifyPassword() ─►│
  │                │                  │── GenerateJWT() ────►│
  │                │                  │── SMTP.Send() ───────► [email LOGIN_SUCCESS al colaborador]
  │◄── { accessToken,                 │                    │
  │      refreshToken } ──────────────│                    │
  │                │                  │                    │
  │── POST /collaborators ──►         │                    │
  │   (Bearer: accessToken)           │                    │
  │                │── Create() ──►   │                    │
  │                │                  │── Generate.RandomText(32) ─► contraseña temporal
  │                │                  │── ValidateRole() ───────────►│
  │                │                  │── Hasher.HashPassword() ─────►│
  │                │                  │── SMTP.Send() ───────► [email COLLABORATOR_REGISTER]
  │◄── 200 { CollaboratorDto } ───────│                    │
```

---

## 📧 Sistema de plantillas de email

Las plantillas se almacenan en la tabla `EmailTemplates` de SQL Server y se cargan en memoria al iniciar la aplicación. El flujo es:

```
Startup
  └── EmailTemplateService.Init()
        └── Lee todas las plantillas de BD
        └── Las guarda en EmailTemplateData (singleton en memoria)

En runtime:
  EmailTemplateService.Get("COLLABORATOR_REGISTER", { "password": "abc123" })
    └── Busca en EmailTemplateData
    └── Interpola {{password}} → "abc123" en el Body
    └── Devuelve EmailTemplateDto con Subject + Body listo para enviar
```

---

## 🔄 Sistema de roles y permisos

El sistema implementa RBAC (Role-Based Access Control) con granularidad por especificidad:

```
Collaborator ──► CollaboratorRoles ──► Role ──► RolePermissions ──► Permission
                                                                        │
                                                                   Specificity:
                                                                   Own | ByAssignment | Creator
```

La validación de permisos en `CollaboratorService`:
- Un `HR` no puede asignar el rol `Admin` (validado en `ValidateRole`).
- El executor (colaborador autenticado) se obtiene desde el claim JWT y se usa para validar que tenga autoridad para ejecutar la acción.

---

## 🚀 Ejecución

```bash
cd backend/TalentInsights

# Restaurar dependencias
dotnet restore

# Levantar la API
dotnet run --project TalentInsights.WebApi

# La API queda disponible en:
# http://localhost:5000
# https://localhost:5001
```

> Al iniciar, la aplicación ejecuta automáticamente:
> 1. `EmailTemplateService.Init()` — carga las plantillas de email en memoria.
> 2. `CollaboratorService.CreateFirstUser()` — crea el primer colaborador Admin si no existe ninguno, usando los datos de `FirstAppTimeUser` en `appsettings.json`.

---

## 📮 Colección Postman

El repositorio incluye dos versiones de la colección Postman:
- `api.postman_collection.json` — versión básica en la raíz.
- `utils/api.postman_collection.json` — versión extendida con más casos de prueba.

Importar cualquiera en Postman y configurar la variable `baseUrl` con la URL de la API.

---

## 🔒 Secretos de la aplicación
Gestiona las configuraciones sensibles del proyecto (claves JWT, credenciales SMTP y cadenas de conexión) utilizando el almacenamiento local de secretos de .NET (`user-secrets`) para evitar exponer credenciales críticas en el control de versiones.
```json
{
  "Jwt": {
    "PrivateKey": "private-key"
  },
  "SMTP": {
    "Host": "host",
    "From": "example@example.com",
    "Port": "587",
    "User": "user",
    "Password": "password"
  },
  "ConnectionStrings": {
    "Database": "connection-string"
  }
}
```

---

## 🛠️ Scaffolding
Comando utilizado para la ingeniería inversa de la base de datos SQL Server hacia el proyecto `TalentInsights.Domain`. Permite regenerar automáticamente las entidades C# y el contexto de EF Core.
```bash
dotnet ef dbcontext scaffold "Server=localhost,1433;User=sa;Password=Admin1234@;Database=TalentInsights;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --project TalentInsights.Domain --startup-project TalentInsights.WebApi --context-dir Database/SqlServer/Context --output-dir Database/SqlServer/Entities --no-build --force --no-onconfiguring
```

---

## 🔗 Enlaces Útiles
- https://http.cat
- https://refactoring.guru/design-patterns
- https://www.uuidgenerator.net
- https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validation-with-the-data-annotation-validators-cs
- https://serilog.net
- https://www.lastpass.com/es/features/password-generator
- https://github.com/alenj0x1/net-ef

---

## 📝 Notas adicionales

- El borrado de colaboradores y mensajes es **lógico** (soft delete mediante `DeletedAt`).
- Las contraseñas de nuevos colaboradores son **generadas aleatoriamente** y enviadas por email; el colaborador debe cambiarla en su primer acceso.
- El SMTP se registra como **Singleton** porque su configuración no cambia durante la vida de la aplicación.
- La cadena de conexión puede venir de una **variable de entorno** (prioridad) o de `appsettings.json`, lo que facilita el despliegue en contenedores.
- La validación de modelos fallida es formateada por `InvalidModelStateResponseFactory` en `AddCore`, devolviendo siempre el mismo formato `GenericResponse` estándar.
- La respuesta de `Get(FilterColaboratorRequest)` incluye el campo `count` (total sin paginación) para facilitar la paginación del lado del cliente.