using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

// Cria o builder da aplicação web - responsável por configurar serviços
var builder = WebApplication.CreateBuilder(args);

// Configuração de Injeção de Dependência (DI)
// AddScoped: Cria uma instância por requisição HTTP
builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();

// Configuração do Entity Framework Core com MySQL
builder.Services.AddDbContext<DbContexto>(options =>
{
    // UseMySql: Configura o provider MySQL com Pomelo
    // GetConnectionString: Busca a string de conexão no appsettings.json
    // ServerVersion.AutoDetect: Detecta automaticamente a versão do MySQL
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

// Constrói a aplicação web com todas as configurações
var app = builder.Build();

// Endpoint GET na raiz - simples teste de funcionamento
app.MapGet("/", () => "Welcome to This Minimal API!");

// Endpoint POST para login
app.MapPost(
    "/login",
    // Parâmetros: DTO do body + serviço injetado automaticamente pelo DI
    ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
    {
        // Chama o método Login do serviço - retorna Administrador ou null
        if (administradorServico.Login(loginDTO) != null)
            return Results.Ok("Login com sucesso!"); // HTTP 200
        else
            return Results.Unauthorized(); // HTTP 401
    }
);

// Inicia a aplicação e fica "escutando" requisições HTTP
app.Run();
