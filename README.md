# User Management API

A secure REST API for managing user records built with ASP.NET Core minimal APIs, featuring token-based authentication, comprehensive validation, and professional middleware architecture.

**Author:** Andre Blankholm

## What This App Does

This application provides a complete user management system with:

- **CRUD Operations**: Create, read, update, and delete user records
- **Token Authentication**: Secure API access with Bearer token validation
- **Data Validation**: Comprehensive input validation with detailed error messages
- **Performance Optimization**: Dictionary-based storage for O(1) lookup operations
- **Error Handling**: Consistent JSON error responses for all failures
- **Request Logging**: Complete audit trail of all API requests and responses

## User Model

Each user contains:
- **ID** (auto-generated integer)
- **Name** (required, 1-100 characters)
- **Title** (required, 1-100 characters)
- **Email** (required, valid email format, max 255 characters)

## Middleware Architecture

The application uses a professional middleware pipeline in this order:

### 1. Error Handling Middleware
- Catches all unhandled exceptions
- Returns consistent JSON error responses: `{ "error": "Internal server error." }`
- Logs exception details for debugging

### 2. Authentication Middleware
- Validates Bearer tokens for all API endpoints
- Returns 401 responses for missing or invalid tokens
- Allows public access to home page and documentation
- Valid tokens: `Bearer valid-token-123` or `Bearer admin-token-456`

### 3. Logging Middleware
- Records HTTP method, request path, and response status code
- Provides complete audit trail: `[timestamp] METHOD /path - statusCode`
- Logs all requests including successful and failed attempts

## API Endpoints

| Method | Endpoint | Description | Authentication |
|--------|----------|-------------|----------------|
| GET | / | Home page | Public |
| GET | /api/users | Get all users | Required |
| GET | /api/users/{id} | Get user by ID | Required |
| POST | /api/users | Create new user | Required |
| PUT | /api/users/{id} | Update user | Required |
| DELETE | /api/users/{id} | Delete user | Required |

## Quick Start

1. Run the application:
   ```
   dotnet run
   ```

2. The API will be available at:
   - HTTPS: https://localhost:7124
   - HTTP: http://localhost:5124

3. Test endpoints using the included `UserManagementAPI.http` file or tools like Postman.

## Authentication

All API endpoints (except home page) require a valid Bearer token:

```
Authorization: Bearer valid-token-123
```

## Example Usage

Create a user:
```json
POST /api/users
Authorization: Bearer valid-token-123
Content-Type: application/json

{
  "name": "John Doe",
  "title": "Software Developer",
  "email": "john.doe@example.com"
}
```

Response:
```json
{
  "id": 1,
  "name": "John Doe",
  "title": "Software Developer",
  "email": "john.doe@example.com"
}
```

## Technology Stack

- ASP.NET Core 9.0
- Minimal APIs
- In-memory data storage
- OpenAPI/Swagger documentation

## Notes

Data is stored in memory and will be lost when the application restarts. This makes it ideal for development, testing, and learning purposes.

## Development Notes

This project was built with assistance from GitHub Copilot, which helped with code generation, API endpoint design, and project structure recommendations. The collaboration between human creativity and AI assistance made the development process more efficient and educational.

## License

Copyright (c) 2025 Andre Blankholm. This software is provided "as is" without warranty of any kind. You may use, modify, and distribute this software freely with proper attribution.