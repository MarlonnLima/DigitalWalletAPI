using DigitalWalletAPI.Domain.Repositories;
using DigitalWalletAPI.Domain.Services;
using DigitalWalletAPI.Infraestructure;
using DotNetEnv;
using Npgsql;
var builder = WebApplication.CreateBuilder(args);

// Setup postgres connectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Env.Load();

var postgresIp = Env.GetString("POSTGRES_IP");
var postgresPort = Env.GetString("POSTGRES_PORT");
var postgresDb = Env.GetString("POSTGRES_DB");
var postgresUser = Env.GetString("POSTGRES_USER");
var postgresPassword = Env.GetString("POSTGRES_PASSWORD");

connectionString = connectionString
        .Replace("POSTGRES_IP", postgresIp)
        .Replace("POSTGRES_PORT", postgresPort)
        .Replace("POSTGRES_DB", postgresDb)
        .Replace("POSTGRES_USER", postgresUser)
        .Replace("POSTGRES_PASSWORD", postgresPassword);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddSingleton(sp => new DbConnectionFactory(connectionString));
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
