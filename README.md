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

## Download & Test

### Clone the Repository
```bash
git clone https://github.com/AndreBlankholm/UserManagementAPI.git
cd UserManagementAPI
```

### Run the Application
```bash
dotnet restore
dotnet run
```

### Test Functionality

**Option 1: Use VS Code REST Client**
- Open `UserManagementAPI.http` in VS Code with the REST Client extension
- Click "Send Request" on any endpoint to test

**Option 2: Test with cURL**
```bash
# Get all users
curl -H "Authorization: Bearer valid-token-123" https://localhost:7124/api/users

# Create a user
curl -X POST https://localhost:7124/api/users \
  -H "Authorization: Bearer valid-token-123" \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","title":"Developer","email":"john@example.com"}'
```

**Option 3: Swagger UI**
- Navigate to: https://localhost:7124/swagger
- Click "Authorize" and enter: `api-key-12345`
- Test all endpoints directly in the browser

## For Testers & Developers

### 🚀 This App is Public & Ready to Test!

**Good News:** This API is designed for public testing! Anyone can:
- ✅ Clone this repository to any device
- ✅ Run the app locally (`dotnet run`)
- ✅ Test all endpoints using the provided API keys
- ✅ No registration or special access required

### 📋 Testing Instructions for Others

**Step 1: Get the Code**
```bash
git clone https://github.com/AndreBlankholm/UserManagementAPI.git
cd UserManagementAPI
```

**Step 2: Run on Your Device**
```bash
dotnet restore
dotnet run
```

**Step 3: Use These Public API Keys**
The app includes these test API keys that work for everyone:
- `api-key-12345` (standard access)
- `admin-api-key-67890` (admin access)

**Step 4: Test Any Endpoint**
```bash
# Example: Get all users
curl -H "X-API-Key: api-key-12345" http://localhost:5124/api/users

# Example: Create a user
curl -X POST http://localhost:5124/api/users \
  -H "X-API-Key: api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{"name":"Test User","title":"Tester","email":"test@example.com"}'
```

### 🔐 Security Notes for Testers

- **API Keys are public** - visible in the source code for easy testing
- **No personal data required** - test freely without registration
- **Device independent** - works on Windows, Mac, Linux
- **Local only** - your test data stays on your machine
- **Memory storage** - data resets when you restart the app

This setup makes it perfect for:
- 🎓 Learning API development
- 🧪 Testing API concepts
- 🔍 Code review and examination
- 📝 Portfolio demonstrations

## Step-by-Step Walkthrough

### 1. Start the Application
After running `dotnet run`, you should see:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7124
      Now listening on: http://localhost:5124
```

### 2. Visit the Home Page
- Open browser to: http://localhost:5124
- You should see: "Welcome to User Management API!"
- This confirms the app is running (no authentication needed)

### 3. Check Swagger Documentation
- Navigate to: https://localhost:7124/swagger
- Browse all available endpoints
- Click "Authorize" and enter: `api-key-12345`

### 4. Make Your First API Call
**Get all users (initially empty):**
```bash
curl -H "X-API-Key: api-key-12345" http://localhost:5124/api/users
```
Expected response: `[]` (empty array)

### 5. Create Your First User
```bash
curl -X POST http://localhost:5124/api/users \
  -H "X-API-Key: api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{"name":"Alice Smith","title":"Product Manager","email":"alice@company.com"}'
```
Expected response:
```json
{"id":1,"name":"Alice Smith","title":"Product Manager","email":"alice@company.com"}
```

### 6. Verify the User Was Created
```bash
curl -H "X-API-Key: api-key-12345" http://localhost:5124/api/users
```
Expected response:
```json
[{"id":1,"name":"Alice Smith","title":"Product Manager","email":"alice@company.com"}]
```

### 7. Get User by ID
```bash
curl -H "X-API-Key: api-key-12345" http://localhost:5124/api/users/1
```

### 8. Update the User
```bash
curl -X PUT http://localhost:5124/api/users/1 \
  -H "X-API-Key: api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{"name":"Alice Johnson","title":"Senior Product Manager","email":"alice.johnson@company.com"}'
```

### 9. Test Authentication (Optional)
Try without API key to see 401 error:
```bash
curl http://localhost:5124/api/users
```
Expected response: `{"error":"API Key header is required."}`

### 10. Delete the User
```bash
curl -X DELETE http://localhost:5124/api/users/1 \
  -H "X-API-Key: api-key-12345"
```

### 11. Verify Deletion
```bash
curl -H "X-API-Key: api-key-12345" http://localhost:5124/api/users
```
Expected response: `[]` (empty again)

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