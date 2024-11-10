using BackEngin.Data;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables.
Env.Load("../.env");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = $"Server={Environment.GetEnvironmentVariable("MSSQL_SERVER")},{Environment.GetEnvironmentVariable("MSSQL_HOST_PORT")};" +
                           $"Database={Environment.GetEnvironmentVariable("MSSQL_DATABASE")};" +
                           $"User Id={Environment.GetEnvironmentVariable("MSSQL_USER")};" +
                           $"Password={Environment.GetEnvironmentVariable("MSSQL_PASSWORD")};" +
                           $"TrustServerCertificate={Environment.GetEnvironmentVariable("TrustServerCertificate")};";
    options.UseSqlServer(connectionString);
});

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
