using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Minimal_API.API.Endpoints;
using Minimal_API.Application;
using Minimal_API.Infrastructure;
using Minimal_API.Persistance;
using Minimal_API.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapUserEndpoints();
app.MapAuthEndpoints();

app.Run();