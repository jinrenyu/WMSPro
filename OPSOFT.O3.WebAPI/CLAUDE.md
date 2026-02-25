# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Build the solution
dotnet build

# Run the API project
dotnet run --project src/OPSOFT.O3.WebAPI.Api

# Run with watch mode (auto-reload on changes)
dotnet watch run --project src/OPSOFT.O3.WebAPI.Api
```

## Architecture

This is a .NET 10 Web API project following Domain-Driven Design (DDD) architecture with four layers:

```
src/
├── OPSOFT.O3.WebAPI.Domain/        # Domain layer - Entities, Interfaces
├── OPSOFT.O3.WebAPI.Application/   # Application layer - DTOs, Services, Interfaces
├── OPSOFT.O3.WebAPI.Infrastructure/# Infrastructure layer - Repositories, External Services
└── OPSOFT.O3.WebAPI.Api/           # Presentation layer - Controllers, Configuration
```

**Layer Dependencies:**
- Domain: No dependencies (core business entities)
- Application: References Domain
- Infrastructure: References Domain and Application
- Api: References Application and Infrastructure

## Key Technologies

- **ORM**: SqlSugar (SqlSugarCore)
- **Authentication**: JWT Bearer tokens
- **API Documentation**: Swagger/OpenAPI
- **Database**: SQL Server

## Configuration

Database connection and JWT settings are in `src/OPSOFT.O3.WebAPI.Api/appsettings.json`:
- `ConnectionStrings:DefaultConnection` - SQLite is used during the development phase, while SQLServer or PGSQL will be employed in the production environment later
- `JwtSettings` - JWT token configuration (SecretKey, Issuer, Audience, ExpirationMinutes)

## Entity Mapping

SqlSugar entity attributes map C# properties to database columns:
- `[SugarTable("TABLE_NAME")]` - Maps class to table
- `[SugarColumn(ColumnName = "COLUMN_NAME")]` - Maps property to column
