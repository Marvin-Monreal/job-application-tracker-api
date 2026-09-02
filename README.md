# Job Application Tracker — API

An ASP.NET Core Web API backend for a full-stack Job Application Tracker designed to manage job applications, interviews, notes, authentication, and job-search data.

The API provides RESTful endpoints consumed by the Angular frontend and uses Entity Framework Core to communicate with PostgreSQL.

## Project Overview

The Job Application Tracker API is the backend component of a full-stack portfolio project.

Its primary responsibilities are:

- Exposing RESTful API endpoints
- Handling business logic
- Validating requests
- Enforcing authorization
- Managing job application data
- Managing interviews and notes
- Providing dashboard statistics
- Communicating with PostgreSQL through Entity Framework Core

### Related Repository

**Frontend UI:** `job-application-tracker-ui`

> [job-application-tracker-ui repository.](https://github.com/Marvin-Monreal/job-application-tracker-ui)

---

## Features

### Authentication & Authorization

The API supports authenticated access to user-owned resources.

Authentication is integrated with Supabase Auth.

The API is responsible for ensuring that authenticated users can only access resources that belong to them.

### Job Applications

The API supports:

- Creating applications
- Retrieving applications
- Retrieving individual applications
- Updating applications
- Deleting applications
- Searching applications
- Filtering applications
- Sorting applications
- Updating application status

### Interviews

The API supports interview management including:

- Interview scheduling
- Interview type
- Interviewer information
- Meeting links
- Interview notes
- Interview results
- Follow-up dates

### Notes

Users can create and manage notes associated with job applications.

### Dashboard

The API provides summary information such as:

- Total applications
- Applications by status
- Interviews
- Offers
- Accepted applications
- Rejected applications
- Upcoming interviews

---

## Technology Stack

| Technology | Purpose |
|---|---|
| ASP.NET Core | Web API framework |
| C# | Backend programming language |
| Entity Framework Core | ORM / data access |
| PostgreSQL | Relational database |
| Supabase | PostgreSQL hosting and authentication |
| Swagger / OpenAPI | API documentation |
| Postman | API testing |
| Git | Version control |
| GitHub | Source control |
| Render | Backend hosting |

---

## Architecture

The API follows a layered architecture to separate responsibilities.

```text
┌─────────────────────────────────────┐
│          ASP.NET Core API           │
│                                     │
│ Controllers / Middleware            │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│        Application Layer            │
│                                     │
│ Services / DTOs / Validators        │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│           Domain Layer              │
│                                     │
│ Entities / Enums / Business Rules   │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│       Infrastructure Layer          │
│                                     │
│ EF Core / Repositories / Database   │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│            PostgreSQL               │
│             Supabase                │
└─────────────────────────────────────┘
```

The application follows a modular-monolith approach rather than introducing unnecessary microservices.

---

## Project Structure

```text
backend/
│
├── JobTracker.Api/
│   ├── Controllers/
│   ├── Middleware/
│   ├── Extensions/
│   └── Program.cs
│
├── JobTracker.Application/
│   ├── DTOs/
│   ├── Services/
│   ├── Interfaces/
│   └── Validators/
│
├── JobTracker.Domain/
│   ├── Entities/
│   ├── Enums/
│   └── Constants/
│
├── JobTracker.Infrastructure/
│   ├── Data/
│   ├── Repositories/
│   ├── Configurations/
│   └── Migrations/
│
└── tests/
    ├── JobTracker.UnitTests/
    └── JobTracker.IntegrationTests/
```

### API Layer

Responsible for HTTP communication and API concerns.

Examples:

- Controllers
- Middleware
- HTTP status codes
- Authentication integration
- API configuration

### Application Layer

Responsible for application use cases and business operations.

Examples:

- Services
- DTOs
- Validators
- Interfaces

### Domain Layer

Contains the core business concepts of the application.

Examples:

- JobApplication
- Interview
- Note
- ApplicationStatus
- EmploymentType
- WorkArrangement

### Infrastructure Layer

Responsible for external implementation details.

Examples:

- Entity Framework Core
- PostgreSQL
- Database configuration
- Migrations
- Repository implementations

---

## Database

The application uses PostgreSQL as its primary relational database.

Initial entities include:

```text
User
 │
 └── JobApplication
       ├── Interview
       └── Note
```

### JobApplication

```text
Id
UserId
CompanyName
JobTitle
Description
JobUrl
Location
WorkArrangement
EmploymentType
SalaryMin
SalaryMax
ApplicationDate
Status
ContactName
ContactEmail
Source
Priority
CreatedAt
UpdatedAt
```

### Interview

```text
Id
JobApplicationId
ScheduledAt
InterviewType
Interviewer
MeetingUrl
Notes
Result
FollowUpDate
CreatedAt
UpdatedAt
```

### Note

```text
Id
JobApplicationId
Content
CreatedAt
UpdatedAt
```

Entity relationships and database constraints are managed using Entity Framework Core.

---

## Security

Security is enforced primarily on the backend.

The API must never rely solely on the frontend to determine whether a user is authorized.

For protected resources, the API verifies:

1. The request is authenticated.
2. The requested resource exists.
3. The resource belongs to the authenticated user.
4. The requested operation is allowed.

This prevents users from accessing another user's application by manipulating resource IDs.

Sensitive configuration values such as database credentials must be provided through environment variables or secure deployment configuration.

---

## API Endpoints

### Applications

```http
GET    /api/applications
GET    /api/applications/{id}
POST   /api/applications
PUT    /api/applications/{id}
DELETE /api/applications/{id}
```

### Application Status

```http
PATCH /api/applications/{id}/status
```

### Interviews

```http
GET    /api/applications/{applicationId}/interviews
POST   /api/applications/{applicationId}/interviews
PUT    /api/interviews/{id}
DELETE /api/interviews/{id}
```

### Notes

```http
GET    /api/applications/{applicationId}/notes
POST   /api/applications/{applicationId}/notes
PUT    /api/notes/{id}
DELETE /api/notes/{id}
```

### Dashboard

```http
GET /api/dashboard/summary
GET /api/dashboard/upcoming-interviews
```

### Profile

```http
GET /api/profile
PUT /api/profile
```

More detailed API documentation is available through Swagger/OpenAPI.

---

## Swagger / OpenAPI

Swagger is used during development to document and test the API.

After starting the application, Swagger should be available at the configured Swagger URL.

Typical development URL:

```text
https://localhost:<port>/swagger
```

Swagger documents:

- Endpoints
- Request parameters
- Request bodies
- Response models
- HTTP status codes
- Authentication requirements

---

## Getting Started

### Prerequisites

Install:

- .NET SDK
- PostgreSQL or access to a Supabase PostgreSQL database
- Git
- Visual Studio / Visual Studio Code
- Postman (recommended)

Verify the .NET installation:

```bash
dotnet --version
```

### Clone the Repository

```bash
git clone https://github.com/<your-username>/job-tracker-api.git
cd job-tracker-api
```

### Configure Environment

The application requires configuration for the database and authentication.

Do not commit production credentials or secrets to GitHub.

Example configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_DATABASE_CONNECTION_STRING"
  }
}
```

For local development, use an appropriate local configuration or user secrets.

### Restore Dependencies

```bash
dotnet restore
```

### Apply Database Migrations

After configuring the database:

```bash
dotnet ef database update
```

### Run the API

```bash
dotnet run
```

The API will start using the configured HTTP/HTTPS development URLs.

---

## Testing

The project is intended to contain multiple levels of testing.

### Unit Tests

Business logic should be tested independently of external infrastructure.

### Integration Tests

Integration tests should verify interactions between the API and its dependencies.

### API Testing

Postman can be used to manually test endpoints and authentication flows.

---

## Deployment

The backend is intended to be deployed using **Render**.

Deployment architecture:

```text
GitHub
   │
   ▼
Render
   │
   ▼
ASP.NET Core Web API
   │
   │ Entity Framework Core
   ▼
Supabase PostgreSQL
```

The deployed API should be configured using production environment variables.

---

## Documentation

Additional technical documentation is maintained in the `docs` directory.

```text
docs/
├── requirements.md
├── architecture.md
└── api-spec.md
```

### Requirements

Defines the functional and non-functional requirements of the application.

### Architecture

Documents the system architecture, application layers, authentication flow, database design, and deployment architecture.

### API Specification

Documents the REST API endpoints, request structures, responses, and HTTP status codes.

---

## Project Goals

This project is being developed as a portfolio project to demonstrate practical backend and full-stack development skills in:

- C#
- ASP.NET Core
- RESTful API development
- Entity Framework Core
- PostgreSQL
- Authentication and authorization
- API validation
- Dependency injection
- Layered architecture
- Database design
- Swagger/OpenAPI
- API testing
- Git/GitHub
- Cloud deployment

---

## Project Status

**In Development**

The API is being developed incrementally alongside the Angular frontend.

---

## Author

**Marvin James Monreal**

Full-Stack / Web Developer

GitHub: `https://github.com/MJMonreal`

LinkedIn: `https://www.linkedin.com/in/marvin-james-monreal-9a5037283/`

---

## License

This project is currently intended as a personal portfolio and learning project.
