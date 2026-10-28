# Sales Lead Management API

## Overview

A RESTful Sales Lead Management API developed for the Keyloop Software Engineer (.NET) technical coding challenge.

The application helps dealership salespeople manage incoming customer enquiries and record follow-up activities.

## Features

- Retrieve the sales lead inbox.
- Create sales leads.
- Retrieve individual lead details.
- Record follow-up activities.
- Retrieve chronological activity history.
- Persist data using SQL Server and Entity Framework Core.
- Validate API request models.
- Test core business logic using xUnit.

## Technology Stack

- C#
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core 8
- SQL Server
- Swagger / OpenAPI
- xUnit
- EF Core InMemory provider for isolated unit tests

## Project Structure

```text
SalesLeadManagement/
├── SalesLeadManagement.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Migrations/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
├── SalesLeadManagement.Tests/
│   └── LeadServiceTests.cs
└── README.md
```

## Prerequisites

- Visual Studio 2022 with support for .NET 8
- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- Entity Framework Core tools

## Database Setup

1. Configure `DefaultConnection` in `appsettings.json` for your SQL Server instance.
2. Set `SalesLeadManagement.Api` as the startup project.
3. Open Tools → NuGet Package Manager → Package Manager Console.
4. Select `SalesLeadManagement.Api` as the default project.
5. Apply the database migration:

```powershell
Update-Database
```

The initial migration creates the database schema for leads and lead activities.

## Running the Application

1. Open `SalesLeadManagement.sln` in Visual Studio 2022.
2. Restore NuGet packages.
3. Build the solution.
4. Set `SalesLeadManagement.Api` as the startup project.
5. Press F5.
6. Open the Swagger URL displayed by the application.

## API Endpoints

| Method | Endpoint                     | Description                                |
| ------ | ---------------------------- | ------------------------------------------ |
| GET    | `/api/Leads`                 | Retrieve all leads                         |
| POST   | `/api/Leads`                 | Create a lead                              |
| GET    | `/api/Leads/{id}`            | Retrieve lead details and activity history |
| GET    | `/api/Leads/{id}/activities` | Retrieve activity history                  |
| POST   | `/api/Leads/{id}/activities` | Add a follow-up activity                   |

## Example: Create a Lead

POST `/api/Leads`

```json
{
  "customerName": "Example Customer",
  "email": "customer@example.com",
  "phone": "9000000001",
  "vehicleInterest": "Hyundai Creta"
}
```

A successful creation returns HTTP 201 Created.

## Example: Add a Follow-up Activity

POST `/api/Leads/1/activities`

```json
{
  "activityType": "Call",
  "description": "Discussed vehicle pricing with the customer."
}
```

Replace `1` with the actual lead ID returned by the API.

## Testing

The solution includes an xUnit test project named `SalesLeadManagement.Tests`.

To run tests:

1. Open Test → Test Explorer in Visual Studio.
2. Select Run All Tests.

The current test suite has five passing tests covering lead creation, lead retrieval, activity creation, and missing-resource scenarios.

Tests use EF Core InMemory to avoid modifying the development SQL Server database.

## Observability

The application uses ASP.NET Core's standard logging infrastructure. Additional structured business-event logging, metrics, distributed tracing, and centralized log aggregation can be added as future improvements.

## AI Collaboration Narrative

Generative AI was used as a development collaborator to help plan the API structure, draft entity and DTO classes, develop service-layer logic, and prepare unit tests.

The implementation was reviewed and refined through Visual Studio builds, Swagger API testing, and automated xUnit tests. The API was manually tested to verify lead creation, retrieval, activity persistence, and activity history.

The development process used isolated in-memory databases for unit tests so that automated testing would not create duplicate records in the development SQL Server database.

AI-generated suggestions were treated as drafts and checked against the application's requirements and observed test results. Final responsibility for understanding, validating, and maintaining the code remains with the developer.

## Assumptions and Limitations

- Authentication and authorization are not currently implemented.
- Incoming leads are created through the API; an external website integration is not included.
- The current implementation is intended as a coding-challenge solution, not a production deployment.
- Production deployment would require additional security, monitoring, operational configuration, and reliability controls.
