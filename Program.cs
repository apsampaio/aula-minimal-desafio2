using API;
using API.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDatabase();
builder.Services.DependencyInjection();
builder.Services.ConfigureCORS();

builder.Services.RegisterAuthentication();
builder.Services.RegisterAuthorization();

var app = builder.Build();

app.UseCors("CORS");

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureRoutes();

app.Run("http://localhost:3002");
