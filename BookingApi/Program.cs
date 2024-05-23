using BookingAPI.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;
using BookingAPI.Managers;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

Env.TraversePath().Load();

string jwtTokenSecret = Environment.GetEnvironmentVariable("TOKEN") ?? throw new ArgumentNullException();

string cosmosConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? throw new ArgumentNullException();
string cosmosDatabaseName = Environment.GetEnvironmentVariable("DATABASE_ID") ?? throw new ArgumentNullException();
builder.Services.AddDbContext<BookingDbContext>(options => options.UseCosmos(cosmosConnectionString, cosmosDatabaseName));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer { TOKEN }",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

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
