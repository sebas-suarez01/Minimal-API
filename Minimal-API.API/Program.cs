using Minimal_API.API.BackgroundServices;
using Minimal_API.API.Endpoints;
using Minimal_API.Application;
using Minimal_API.Infrastructure;
using Minimal_API.Persistance;
using Minimal_API.API.Configuration;
using Minimal_API.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<BackgroundServiceCountItems>();

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddCacheConfiguration(builder.Configuration);

builder.Services.AddFixedWindowRateLimiter();
builder.Services.AddConcurrencyGlobalRateLimiter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// Middlewares
//app.UseGlobalExceptionHandler();
app.UseExceptionHandler(exc => exc.ConfigureExceptionHandler());
app.UseTiming();

// Endpoints
app.MapUserEndpoints();
app.MapAuthEndpoints();
app.MapOrderEndpoints();
app.MapItemEndpoints();

app.Run();