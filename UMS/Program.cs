using Microsoft.EntityFrameworkCore;
using UMS.EF.Tables;
using UMS.Data;

var builder = WebApplication.CreateBuilder(args);

// --- 1. REGISTER SERVICES (Before builder.Build) ---
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger/OpenAPI services
builder.Services.AddSwaggerGen();

// Fix: Move DB context and Controller registration here
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

// --- 2. BUILD THE APP ---
var app = builder.Build();

// --- 3. CONFIGURE PIPELINE (After builder.Build) ---
// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "UMS API v1");
    options.RoutePrefix = "swagger"; // Access Swagger UI at /swagger
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var httpsPort = builder.Configuration["ASPNETCORE_HTTPS_PORT"];
if (!string.IsNullOrWhiteSpace(httpsPort))
{
    app.UseHttpsRedirection();
}

// Map Controllers (Required if you are using Controller classes)
app.MapControllers();

// Endpoints
app.MapGet("/", () => Results.Ok(new { status = "ok" }));

app.MapGet("/weatherforecast", () =>
{
    var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapFallback(() => Results.Ok(new { status = "ok" }));

app.Run();

// Records/Models
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}