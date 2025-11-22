using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to listen on port 8080 inside Docker
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Services
builder.Services.AddControllers();

// New OpenAPI (built-in)
builder.Services.AddOpenApi();

// Classic Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Thevar API",
        Version = "v1"
    });
});

var app = builder.Build();

// Classic Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thevar API v1");
    c.RoutePrefix = "swagger";
});

// New OpenAPI JSON
app.MapOpenApi();

// Docker: no HTTPS redirection
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
