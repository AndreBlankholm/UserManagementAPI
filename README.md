# User Management API

A simple REST API for managing user records built with ASP.NET Core and minimal APIs.

**Author:** Andre Blankholm

## Overview

This application provides basic CRUD (Create, Read, Update, Delete) operations for user management. It stores user data in memory and offers a lightweight solution for testing and learning API development.

## Features

- Create new users
- Retrieve all users or specific users by ID
- Update existing user information
- Delete users
- Simple home page endpoint

## User Model

Each user contains:
- ID (auto-generated)
- Name
- Title

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | / | Home page |
| GET | /api/users | Get all users |
| GET | /api/users/{id} | Get user by ID |
| POST | /api/users | Create new user |
| PUT | /api/users/{id} | Update user |
| DELETE | /api/users/{id} | Delete user |

## Quick Start

1. Run the application:
   ```
   dotnet run
   ```

2. The API will be available at:
   - HTTPS: https://localhost:7124
   - HTTP: http://localhost:5124

3. Test endpoints using the included `UserManagementAPI.http` file or tools like Postman.

## Example Usage

Create a user:
```json
POST /api/users
{
  "name": "John Doe",
  "title": "Software Developer"
}
```

Response:
```json
{
  "id": 1,
  "name": "John Doe",
  "title": "Software Developer"
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