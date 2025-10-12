# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

ParkingLotAPP is a .NET 9.0 web API for managing parking lot operations, including cars, drivers, car brands, and car colors. The project follows Clean Architecture principles with clear separation of concerns across multiple layers.

## Architecture

The solution is organized into 5 projects following Clean Architecture:

- **ParkingLotAPP.Domain**: Core domain entities, interfaces, value objects, and validation logic. No external dependencies.
- **ParkingLotAPP.Application**: Application use cases using MediatR (CQRS pattern). Depends on Domain layer.
- **ParkingLotAPP.Data**: Data access layer with Entity Framework Core and SQL Server. Contains repositories, DbContext, and migrations.
- **ParkingLotAPP.API**: ASP.NET Core Web API presentation layer with controllers, filters, and API models.
- **ParkingLotAPP.UnitTests**: xUnit tests using Bogus for test data generation and FluentAssertions.

### Dependency Flow
API → Application → Domain ← Data

The Data layer references both Application (for IUnitOfWork) and Domain. The API layer references both Application and Data.

### Key Architectural Patterns

**CQRS with MediatR**: All use cases implement MediatR's `IRequestHandler<TRequest, TResponse>`. Use cases are registered via `AddUseCases()` extension method in `Configurations/UseCasesConfiguration.cs`.

**Repository Pattern**: All repositories inherit from `IGenericRepository<TEntity>` in the Domain layer. Implementations are in the Data layer.

**Unit of Work Pattern**: `IUnitOfWork` interface in Application layer, implemented in Data layer as `UnitOfWork`. Wraps DbContext transactions.

**Value Objects**: Domain uses value objects like `Plate` for type-safe encapsulation of business rules.

**Domain Validation**: Entities validate themselves using `DomainValidation` static methods in constructors and update methods.

## Common Commands

### Build and Run
```bash
# Build entire solution
dotnet build

# Run the API (from repository root)
dotnet run --project src/ParkingLotAPP.API/ParkingLotAPP.API.csproj

# Run in watch mode for development
dotnet watch --project src/ParkingLotAPP.API/ParkingLotAPP.API.csproj
```

### Testing
```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run tests in watch mode
dotnet watch test --project tests/ParkingLotAPP.UnitTests/ParkingLotAPP.UnitTests.csproj
```

### Database Migrations
```bash
# Add a new migration (run from repository root)
dotnet ef migrations add <MigrationName> --project src/ParkingLotAPP.Data/ParkingLotAPP.Data.csproj --startup-project src/ParkingLotAPP.API/ParkingLotAPP.API.csproj

# Update database
dotnet ef database update --project src/ParkingLotAPP.Data/ParkingLotAPP.Data.csproj --startup-project src/ParkingLotAPP.API/ParkingLotAPP.API.csproj

# Remove last migration
dotnet ef migrations remove --project src/ParkingLotAPP.Data/ParkingLotAPP.Data.csproj --startup-project src/ParkingLotAPP.API/ParkingLotAPP.API.csproj
```

## Database Configuration

The application uses SQL Server with Entity Framework Core. Connection string is configured in `appsettings.json`:
- Server: `nicolaspc`
- Database: `Console`
- Trusted_Connection with TrustServerCertificate enabled
- Includes retry logic (5 retries with 10-second max delay)

DbContext configuration is split between `ConsoleDbContext.OnModelCreating()` (applying entity configurations) and `Program.cs` (DI registration with connection string from configuration).

## Creating New Features

When adding a new entity/feature, follow this pattern:

1. **Domain Layer** (`src/ParkingLotAPP.Domain/`):
   - Create entity class inheriting from `Entity` in `Entities/`
   - Add validation in entity constructor using `DomainValidation`
   - Create repository interface in `Interfaces/` (e.g., `IXRepository : IGenericRepository<X>`)
   - Add any value objects to `ValueObjects/`

2. **Data Layer** (`src/ParkingLotAPP.Data/`):
   - Create EF Core configuration in `Configurations/` (e.g., `XConfiguration : IEntityTypeConfiguration<X>`)
   - Implement repository in `Repositories/` (e.g., `XRepository : IXRepository`)
   - Add DbSet to `ConsoleDbContext`
   - Apply configuration in `ConsoleDbContext.OnModelCreating()`
   - Create migration: `dotnet ef migrations add <Name> --project src/ParkingLotAPP.Data --startup-project src/ParkingLotAPP.API`
   - Register repository in `Program.cs` DI

3. **Application Layer** (`src/ParkingLotAPP.Application/`):
   - Create use case folder structure: `UseCases/X/CreateX/`, `UpdateX/`, `DeleteX/`, `GetX/`, `ListXs/`
   - For each use case, create: `ICreateX.cs` interface, `CreateXInput.cs`, `CreateX.cs` handler
   - Create common output model in `UseCases/X/Common/XModelOutput.cs`
   - Use cases implement `IRequestHandler<TInput, TOutput>` from MediatR
   - No additional registration needed; MediatR auto-discovers handlers via `AddUseCases()`

4. **API Layer** (`src/ParkingLotAPP.API/`):
   - Create controller in `Controllers/` inheriting `ControllerBase`
   - Inject `IMediator` and call `_mediator.Send(input, cancellationToken)`
   - Wrap responses in `APIResponse<T>` for single items or `APIResponseList<T>` for paginated lists
   - Use `ProducesResponseType` attributes for Swagger documentation

## Important Notes

- All entities inherit from `Entity` base class which provides `Guid Id` property
- Always use `CancellationToken` in async methods
- Repository methods return `Task<TEntity>` (not nullable), throwing `NotFoundException` when not found
- The API uses a global exception filter `APIGlobalExceptionFilter` to handle exceptions consistently
- JSON serialization is configured to ignore cycles with `ReferenceHandler.IgnoreCycles`
- Use `DomainEntity = ParkingLotAPP.Domain.Entities` alias when creating domain entities in use cases to avoid naming conflicts
- Tests use Bogus for generating fake data and FluentAssertions for assertions
- The API serves Swagger UI in development mode at `/openapi/v1.json`
