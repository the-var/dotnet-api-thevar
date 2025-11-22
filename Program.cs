var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to listen on port 8080 inside Docker
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Add services
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Disable HTTPS redirection inside Docker
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
