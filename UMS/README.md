# University Management System (UMS) API

A .NET 10.0 Web API for managing university data with Entity Framework Core and PostgreSQL.

## Features

- RESTful API with ASP.NET Core
- Entity Framework Core with PostgreSQL
- Swagger/OpenAPI documentation
- Student management endpoints

## Prerequisites

- .NET 10.0 SDK
- PostgreSQL database

## Getting Started

### 1. Configure Database Connection

Update the connection string in `appsettings.json` or `.env` file.

### 2. Run Migrations

```bash
dotnet ef database update
```

### 3. Run the Application

```bash
dotnet run
```

## API Documentation

### Swagger UI

Once the application is running, you can access the interactive API documentation at:

**http://localhost:5000/swagger** (or your configured port)

The Swagger UI provides:
- Complete API endpoint documentation
- Interactive testing of all endpoints
- Request/response schemas
- Example values

### Available Endpoints

#### Students API (`/api/students`)

- `GET /api/students` - Get all students
- `GET /api/students/{id}` - Get a specific student by ID
- `POST /api/students` - Create a new student
- `PUT /api/students/{id}` - Update an existing student
- `DELETE /api/students/{id}` - Delete a student

#### Other Endpoints

- `GET /` - Health check
- `GET /weatherforecast` - Sample weather forecast endpoint

## Technology Stack

- **Framework**: .NET 10.0
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 10.0
- **API Documentation**: Swashbuckle.AspNetCore 10.1.0
- **Database Provider**: Npgsql.EntityFrameworkCore.PostgreSQL 10.0

## Project Structure

```
UMS/
├── Controllers/        # API Controllers
├── Data/              # Database Context
├── EF/                # Entity Framework Models
│   └── Tables/        # Database Tables
├── Migrations/        # EF Core Migrations
└── Program.cs         # Application Entry Point
```

## Development

### Adding New Migrations

```bash
dotnet ef migrations add MigrationName
```

### Updating Database

```bash
dotnet ef database update
```

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## License

This project is for academic purposes.
