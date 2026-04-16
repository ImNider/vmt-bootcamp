# Backend
El Backend de este Bootcamp es desarrollado con .NET

## Requisitos
- Visual Studio Community
- Postman

## Recomendaciones
### Generales
- Siempre usar Soluciones para los proyectos.
- Los Proyectos dentro de una Solucion deben tener la estructura SolutionName.ProjectName.
- Si se trabaja con base de datos, no olvidar modificar el appsettings.json del WebApi.

## Flujo recomendado para crear un proyecto
### Capas
- Crear un ProjectName.WebApp como ASP.NET Core Web App
- Crear un ProjectName.Application como Library
- Crear un ProjectName.Shared como Library
- Crear un ProjectName.Domain como Library
- Crear un ProjectName.Infrastructure como Library

### Carpetas de la Capa Application
- Helpers
- Interfaces
	- Services
- Models
	- DTOs
	- Requests
	- Responses
- Services

### Carpetas de la Capa Shared
- Constants
- Helpers

### Carpetas de la Capa Domain
- Database
	- SqlServer
		- Context
		- Entities
- Exceptions
- Interfaces
	- Repositories

### Carpetas de la Capa Infrastructure
- Persistence
	- SqlServer
		- Repositories

### Carpetas de la Capa WebApp
- Extensions
- Middlewares

### Referencias de Proyecto
- Capa Application: Shared y Domain
- Capa Infrastructure: Domain
- Capa WebApp: Application, Domain e Infrastructure

### Instalar Paquetes NuGet
- Capa Domain
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
- Capa WebApp
	- Microsoft.EntityFrameworkCore.Design
	- Serilog
	- Serilog.Extensions.Hosting
	- Serilog.Sinks.File
	- Serilog.Sinks.Console
	- Serilog.Sinks.MSSqlServer

### Secretos de la aplicación
Estos datos se almacenan usando `user-secrets` para no exponerlos en el repositorio.
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

### Comandos en la Terminal
- Scaffolding:
```bash
dotnet ef dbcontext scaffold "Server=localhost,1433;User=sa;Password=Admin1234@;Database=TalentInsights;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --project TalentInsights.Domain --startup-project TalentInsights.WebApi --context-dir Database/SqlServer/Context --output-dir Database/SqlServer/Entities --no-build --force --no-onconfiguring
```

### Archivos (Orden sugerido)
1. Cache.cs - `Shared/` (Solo para caché)
2. ServiceCollectionExtension.cs - `WebApp/Extensions/`
2. DataTimeHelper.cs - `Shared/Helpers/`
3. ValidationConstants.cs - `Shared/Constants/`
4. ResponseConstants.cs - `Shared/Constants/`
5. GenericResponse.cs - `Application/Models/Responses/`
6. ResponsesHelper.cs - `Application/Helpers/`
7. BaseRequest.cs - `Application/Requests/`
8. Exceptions - `Domain/Exceptions/`
9. DTOs - `Application/Models/DTOs/`
10. Requests - `Application/Models/Requests/EntityName/`
11. IRepositories - `Domain/Interfaces/Repositories/`
12. Repositories `Infrastructure/Persistence/SqlServer/Repositories/`
13. IServices - `Application/Interface/Services/`
14. Services - `Application/Services/`
15. ErrorHandlerMiddleware.cs - `WebApp/Middlewares/`
16. Controllers - `WebApp/Controllers/`

## Enlaces Útiles
- https://http.cat
- https://refactoring.guru/design-patterns
- https://www.uuidgenerator.net
- https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validation-with-the-data-annotation-validators-cs
- https://serilog.net
- https://www.lastpass.com/es/features/password-generator
- https://github.com/alenj0x1/net-ef