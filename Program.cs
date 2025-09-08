/// <summary>
/// Aplicação Minimal API para gerenciamento de veículos.
///
/// Esta aplicação implementa uma API REST simples com endpoints para:
/// - Autenticação de administradores via email/senha
/// - CRUD completo de veículos com filtros e paginação
/// - Documentação automática via Swagger/OpenAPI
///
/// Tecnologias utilizadas:
/// - .NET 9 Minimal API
/// - Entity Framework Core com MySQL
/// - Injeção de Dependência nativa
/// - Data Annotations para validação
/// </summary>
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Administradores
app.MapPost(
        "/admin/login",
        ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
        {
            if (administradorServico.Login(loginDTO) != null)
                return Results.Ok("Login com sucesso!");
            else
                return Results.Unauthorized();
        }
    )
    .WithTags("Administradores");
#endregion

#region Veiculos
ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
{
    var validacao = new ErrosDeValidacao { Mensagens = new List<string>() };

    if (string.IsNullOrEmpty(veiculoDTO.Nome))
        validacao.Mensagens.Add("O nome do veículo é obrigatório.");

    if (string.IsNullOrEmpty(veiculoDTO.Marca))
        validacao.Mensagens.Add("A marca do veículo é obrigatória.");

    if (veiculoDTO.Ano < 1886 || veiculoDTO.Ano > DateTime.Now.Year + 1)
        validacao.Mensagens.Add("O ano do veículo é inválido.");

    return validacao;
}

app.MapPost(
        "/veiculos",
        ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
        {
            var validacao = validaDTO(veiculoDTO);
            if (validacao.Mensagens.Count > 0)
                return Results.BadRequest(validacao);

            var veiculo = new Veiculo
            {
                Nome = veiculoDTO.Nome,
                Marca = veiculoDTO.Marca,
                Ano = veiculoDTO.Ano,
            };
            veiculoServico.Incluir(veiculo);

            return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
        }
    )
    .WithTags("Veiculos");

app.MapGet(
        "/veiculos",
        ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
        {
            var veiculos = veiculoServico.Todos(pagina);

            return Results.Ok(veiculos);
        }
    )
    .WithTags("Veiculos");

app.MapGet(
        "/veiculos/{id}",
        ([FromRoute] int id, IVeiculoServico veiculoServico) =>
        {
            var veiculo = veiculoServico.BuscaPorId(id);

            if (veiculo == null)
                return Results.NotFound();

            return Results.Ok(veiculo);
        }
    )
    .WithTags("Veiculos");

app.MapPut(
        "/veiculos/{id}",
        ([FromRoute] int id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
        {
            var veiculo = veiculoServico.BuscaPorId(id);
            if (veiculo == null)
                return Results.NotFound();

            var validacao = validaDTO(veiculoDTO);
            if (validacao.Mensagens.Count > 0)
                return Results.BadRequest(validacao);

            veiculo.Nome = veiculoDTO.Nome;
            veiculo.Marca = veiculoDTO.Marca;
            veiculo.Ano = veiculoDTO.Ano;

            veiculoServico.Atualizar(veiculo);

            return Results.Ok(veiculo);
        }
    )
    .WithTags("Veiculos");

app.MapDelete(
        "/veiculos/{id}",
        ([FromRoute] int id, IVeiculoServico veiculoServico) =>
        {
            var veiculo = veiculoServico.BuscaPorId(id);

            if (veiculo == null)
                return Results.NotFound();

            veiculoServico.Apagar(veiculo);

            return Results.NoContent();
        }
    )
    .WithTags("Veiculos");

#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
#endregion
