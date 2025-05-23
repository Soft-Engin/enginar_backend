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
using System.Runtime.CompilerServices;

// Add environment variables.
Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWTSecretKey")))
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
    // Get the environment variable as a string
    var maxConnString = Environment.GetEnvironmentVariable("MAX_CONNECTION");

    // Try parsing it to an integer
    if (!int.TryParse(maxConnString, out var maxConn))
    {
        maxConn = 10; // fallback or handle parsing error
    }

    // Subtract 10, but make sure it doesn't go below 1
    // The postgre reserves some for superusers and other stuff so we can't use all of them
    // So the max connections should be a little lower than the postgre max connections
    var adjustedMax = Math.Max(1, maxConn - 10);


    var connectionString = $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                           $"Port={Environment.GetEnvironmentVariable("POSTGRES_HOST_PORT")};" +
                           $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                           $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                           $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};" +
                           "Pooling=true;" + // Enable connection pooling (default is true)
                           $"MinPoolSize={Environment.GetEnvironmentVariable("MIN_CONNECTION")};" + // Minimum number of connections in the pool
                           $"MaxPoolSize={adjustedMax};" + // Maximum number of connections in the pool
                           $"ConnectionIdleLifetime={Environment.GetEnvironmentVariable("CONNECTION_LIFETIME")};"; // Time (seconds) a connection can be idle before being closed
    options.UseNpgsql(connectionString);
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
builder.Services.AddScoped<IAllergenService, AllergenService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIngredientsService, IngredientsService>();
builder.Services.AddScoped<IIngredientTypesService, IngredientTypesService>();
builder.Services.AddScoped<IPostInteractionService, PostInteractionService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IFeedService, FeedService>();


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
