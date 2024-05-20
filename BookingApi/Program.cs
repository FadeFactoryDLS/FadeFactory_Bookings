using Microsoft.OpenApi.Models;
using Microsoft.Azure.Cosmos;
using BookingAPI.Services;
using System.Configuration;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

string ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? string.Empty;
string DatabaseId = Environment.GetEnvironmentVariable("DATABASE_ID") ?? string.Empty;
string ContainerId = Environment.GetEnvironmentVariable("CONTAINER_ID") ?? string.Empty;

if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(DatabaseId) || string.IsNullOrEmpty(ContainerId))
{
    throw new Exception("Missing Cosmos DB configuration");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(x => new CosmosClient(ConnectionString));


builder.Services.AddScoped<BookingService>(sp =>
    new BookingService(
        sp.GetRequiredService<CosmosClient>(),
        DatabaseId,
        ContainerId
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
