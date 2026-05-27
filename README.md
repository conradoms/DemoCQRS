DemoCQRS
========

Overview
--------
DemoCQRS is a small .NET 10 sample implementing a CQRS-style architecture for managing Member entities. It demonstrates separation between command and query flows, domain validation, and integration with both EF Core and Dapper for persistence.

Technologies
------------
- .NET 10
- C# (nullable reference types enabled)
- ASP.NET Core Web API
- MediatR (CQRS / mediator pattern)
- Entity Framework Core (PostgreSQL provider via Npgsql)
- Dapper (lightweight queries for read-models)
- FluentValidation (request validation pipeline)
- Npgsql (Postgres ADO.NET driver)
- OpenAPI / Swagger (developer-time API docs)

Architectural patterns and techniques
------------------------------------
- CQRS (Command Query Responsibility Segregation): separate command handlers (create/update/delete) from query handlers (read operations).
- Mediator pattern: MediatR is used to dispatch commands, queries and notifications.
- Repository pattern: IMemberRepository abstracts EF Core operations for write-side work; IMemberDapperRepository exposes read-side queries via Dapper.
- Unit of Work: a simple UnitOfWork wrapper commits EF Core changes in a single transaction (IUnitOfWork).
- Domain model and validation: a Member entity encapsulates domain validation using DomainValidation helper (throws DomainValidation exceptions on invariant violations).
- Validation pipeline: FluentValidation validators are registered and a MediatR pipeline behavior (ValidationBehaviour) runs validations before handlers execute.
- Notifications: domain events / notifications (MemberCreatedNotification) are published via MediatR; multiple INotificationHandler implementations demonstrate side-effects (email/sms logging).
- EF Core entity configuration: MemberConfiguration centralizes schema mapping and seed data.
- Exception handling: a custom ASP.NET Core exception filter maps known exceptions (validation, not found, internal) to HTTP responses.

Project structure (important files)
----------------------------------
- DemoCQRS.API
  - Program.cs (app start, DI registration via CrossCutting)
  - Controllers/MembersController.cs (API endpoints)
  - Filters/CustomExceptionFilter.cs
- DemoCQRS.Application
  - Members.Commands (Create/Update/Delete command handlers, notifications)
  - Members.Queries (GetMembersQuery, GetMemberByIdQuery)
  - Validation (FluentValidation validators, ValidationBehaviour)
- DemoCQRS.Domain
  - Entities/Member.cs (domain entity)
  - Abstractions (IMemberRepository, IMemberDapperRepository, IUnitOfWork)
  - Validation/DomainValidation.cs
- DemoCQRS.Infrastructure
  - Context/AppDbContext.cs (EF Core DbContext)
  - EntityConfiguration/MemberConfiguration.cs
  - Repositories (MemberRepository, MemberDapperRepository, UnitOfWork)
  - Migrations (EF Core migrations)
- DemoCQRS.CrossCutting
  - AppDependencies/DependencyInjection.cs (service registration, DbContext, Dapper connection, MediatR handlers)

How to run
----------
1. Set the PostgreSQL connection string in DemoCQRS.API/appsettings.json (key: DefaultConnection).
2. Apply EF Core migrations (from solution root):
   - dotnet tool install --global dotnet-ef (if not installed)
   - dotnet ef database update --project DemoCQRS.Infrastructure --startup-project DemoCQRS.API
3. Run the API:
   - dotnet run --project DemoCQRS.API
4. In Development environment OpenAPI will be available (Swagger UI) and the Scalar API reference is mapped as in Program.cs.

Notes and extension points
--------------------------
- Read operations use Dapper via IMemberDapperRepository for lightweight queries; write operations use EF Core via IMemberRepository and UnitOfWork.
- ValidationBehaviour integrates FluentValidation into MediatR; add more validators by creating AbstractValidator<T> in the application assembly.
- Notifications demonstrate post-commit side-effects; consider using an outbox pattern for reliability in distributed systems.

License
-------
This project is provided as an example and contains minimal licensing information. Adapt as needed for production use.
