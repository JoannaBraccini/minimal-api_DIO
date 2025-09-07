using Microsoft.EntityFrameworkCore;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost(
    "/login",
    (LoginDTO loginDTO) =>
    {
        if (loginDTO.Email == "admin@teste.com" && loginDTO.Senha == "senha123")
            return Results.Ok("Login com sucesso!");
        else
            return Results.Unauthorized();
    }
);

app.Run();
