using UserManagementAPI.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Error-handling middleware first
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        // Log the exception
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {ex.Message}");
        
        // Set response to JSON and 500 status
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        
        // Return consistent JSON error response
        var errorResponse = new { error = "Internal server error." };
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
});

// Authentication middleware next
app.Use(async (context, next) =>
{
    // Skip authentication for home page and OpenAPI endpoints
    if (context.Request.Path == "/" || context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/openapi"))
    {
        await next();
        return;
    }
    
    // Check for API Key header
    if (!context.Request.Headers.TryGetValue("X-API-Key", out var apiKeyHeader))
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = "API Key header is required." });
        return;
    }
    
    var apiKey = apiKeyHeader.ToString();
    
    // Valid API keys (in production, store these securely hashed in database)
    var validApiKeys = new[] { "api-key-12345", "admin-api-key-67890" };
    if (!validApiKeys.Contains(apiKey))
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = "Invalid API key." });
        return;
    }
    
    // Token is valid, continue to next middleware
    await next();
});

// Logging middleware last
app.Use(async (context, next) =>
{
    var method = context.Request.Method;
    var path = context.Request.Path;
    
    // Call the next middleware
    await next();
    
    var statusCode = context.Response.StatusCode;
    
    // Log the request details
    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {method} {path} - {statusCode}");
});

// Optimized in-memory storage - Dictionary for O(1) lookups
var users = new Dictionary<int, User>();
var nextId = 1;

// Home page
app.MapGet("/", () => "Hi, this is the home page");

// GET /api/users - Get all users
app.MapGet("/api/users", () => users.Values.ToList());

// GET /api/users/{id} - Get user by ID
app.MapGet("/api/users/{id:int}", (int id) =>
{
    if (users.TryGetValue(id, out var user))
        return Results.Ok(user);
    return Results.NotFound();
});

// POST /api/users - Create new user
app.MapPost("/api/users", (User user) =>
{
    // Validate the user data
    var validationContext = new ValidationContext(user);
    var validationResults = new List<ValidationResult>();
    
    if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
    {
        var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
        return Results.BadRequest(new { errors });
    }
    
    user.Id = nextId++;
    users[user.Id] = user;
    return Results.Created($"/api/users/{user.Id}", user);
});

// PUT /api/users/{id} - Update user
app.MapPut("/api/users/{id:int}", (int id, User updatedUser) =>
{
    if (!users.TryGetValue(id, out var user))
        return Results.NotFound();
    
    // Validate the updated user data
    var validationContext = new ValidationContext(updatedUser);
    var validationResults = new List<ValidationResult>();
    
    if (!Validator.TryValidateObject(updatedUser, validationContext, validationResults, true))
    {
        var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
        return Results.BadRequest(new { errors });
    }
    
    user.Name = updatedUser.Name;
    user.Title = updatedUser.Title;
    user.Email = updatedUser.Email;
    return Results.Ok(user);
});

// DELETE /api/users/{id} - Delete user
app.MapDelete("/api/users/{id:int}", (int id) =>
{
    if (!users.Remove(id))
        return Results.NotFound();
    
    return Results.NoContent();
});

app.Run();
