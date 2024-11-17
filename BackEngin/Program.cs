using BackEngin.Data;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Models;
using Asp.Versioning;
using BackEngin.Services.Interfaces;
using BackEngin.Services;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables.
Env.Load("../.env");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure authentication using JWT bearer token
builder.Services.AddAuthentication(options =>
{
    // Set the default schemes for authentication and challenge
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]))
        };
});

// Configure IdentityCore with Entity Framework and Role support
builder.Services.AddIdentityCore<Users>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();


// Configure database context with environment variables
builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = $"Server={Environment.GetEnvironmentVariable("MSSQL_SERVER")},{Environment.GetEnvironmentVariable("MSSQL_HOST_PORT")};" +
                           $"Database={Environment.GetEnvironmentVariable("MSSQL_DATABASE")};" +
                           $"User Id={Environment.GetEnvironmentVariable("MSSQL_USER")};" +
                           $"Password={Environment.GetEnvironmentVariable("MSSQL_PASSWORD")};" +
                           $"TrustServerCertificate={Environment.GetEnvironmentVariable("TrustServerCertificate")};";
    options.UseSqlServer(connectionString);
});

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;  // Assume version 1 by default
    options.DefaultApiVersion = new ApiVersion(1, 0);    // Set the default version
    options.ReportApiVersions = true;                    // Include versioning info in the response
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Use version in the URL path
});

// Register services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<IUrlService, UrlService>();


// Configure Swagger with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token with 'Bearer ' prefix",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"  // Adding the scheme type to clarify
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
