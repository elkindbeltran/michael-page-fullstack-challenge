# Michael Page Fullstack Challenge

Clean Architecture-based backend built with **.NET 9**, **EF Core**, **MediatR**, and **Serilog**.

---

## Architecture

This solution follows **Clean Architecture** principles:

```
src/
 ├── MichaelPageChallenge.API            # Entry point (Controllers, Middleware)
 ├── MichaelPageChallenge.Application    # Use cases (CQRS, MediatR, DTOs)
 ├── MichaelPageChallenge.Domain         # Entities, core business logic
 ├── MichaelPageChallenge.Infrastructure # EF Core, repositories, persistence

tests/
 ├── MichaelPageChallenge.UnitTests      # Unit tests (Handlers, logic)
```

---

## Tech Stack

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core (SQL Server)
* MediatR (CQRS pattern)
* Serilog (structured logging)
* xUnit + Moq + FluentAssertions

---

## Setup & Run

### 1. Configure database

Update connection string in:

```
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MichaelPageDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### 2. Run migrations

Using **Package Manager Console**:

```powershell
Add-Migration InitialCreate -Project MichaelPageChallenge.Infrastructure -StartupProject MichaelPageChallenge.API
Update-Database -Project MichaelPageChallenge.Infrastructure -StartupProject MichaelPageChallenge.API
```

Or using **CLI**:

```bash
dotnet ef migrations add InitialCreate --project src/MichaelPageChallenge.Infrastructure --startup-project src/MichaelPageChallenge.API
dotnet ef database update --project src/MichaelPageChallenge.Infrastructure --startup-project src/MichaelPageChallenge.API
```

---

### 3. Run the API

```bash
dotnet run --project src/MichaelPageChallenge.API
```

Swagger will be available at:

```
https://localhost:<port>/swagger
```

---

## Running Tests

```bash
dotnet test
```

---

## Logging

* Logs are written to:

  ```
  /logs
  ```
* Includes **CorrelationId** per request for traceability

---

## Features

* Clean Architecture separation
* CQRS with MediatR
* Global exception handling
* Correlation ID middleware
* Structured logging with Serilog
* Unit testing for application layer

---

## Notes

This project is designed as a **production-ready baseline** and can be easily extended with:

* Authentication (JWT / Auth0)
* Pagination & filtering
* Multi-tenancy
* Docker support

---
