using BookingAPI.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;
using BookingAPI.Managers;

var builder = WebApplication.CreateBuilder(args);

Env.TraversePath().Load();

string cosmosConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? throw new ArgumentNullException();
string cosmosDatabaseName = Environment.GetEnvironmentVariable("DATABASE_ID") ?? throw new ArgumentNullException();
builder.Services.AddDbContext<BookingDbContext>(options => options.UseCosmos(cosmosConnectionString, cosmosDatabaseName));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookingService, BookingDbManager>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
