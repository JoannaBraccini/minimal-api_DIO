/// <summary>
/// Aplicação Minimal API para gerenciamento de veículos com autenticação JWT.
///
/// Esta aplicação implementa uma API REST segura com endpoints para:
/// - Autenticação de administradores via email/senha com tokens JWT
/// - CRUD completo de veículos com filtros e paginação
/// - Sistema de autorização baseado em perfis de usuário
/// - Documentação automática via Swagger/OpenAPI
///
/// Tecnologias utilizadas:
/// - .NET 9 Minimal API
/// - Entity Framework Core com MySQL
/// - JWT Bearer Authentication
/// - Injeção de Dependência nativa
/// - Data Annotations para validação
/// </summary>
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetSection("Jwt").ToString();
if (string.IsNullOrEmpty(key))
    key = "default_key";

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira o token JWT aqui",
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                // new string[] {}
                Array.Empty<string>()
            },
        }
    );
});

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql"))
    );
});

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
#endregion

#region Administradores
string GerarTokenJwt(Administrador administrador)
{
    if (string.IsNullOrEmpty(key))
        return string.Empty;

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>()
    {
        new("Email", administrador.Email),
        new("Perfil", administrador.Perfil),
    };

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

app.MapPost(
        "/administradores/login",
        ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
        {
            var adm = administradorServico.Login(loginDTO);
            if (adm != null)
            {
                string token = GerarTokenJwt(adm);
                return Results.Ok(
                    new AdministradorLogado
                    {
                        Email = adm.Email,
                        Perfil = adm.Perfil,
                        Token = token,
                    }
                );
            }
            else
                return Results.Unauthorized();
        }
    )
    .AllowAnonymous()
    .WithTags("Administradores");

app.MapGet(
        "/administradores/",
        ([FromQuery] int? pagina, IAdministradorServico administradorServico) =>
        {
            var adms = new List<AdministradorModelView>();
            var administradores = administradorServico.Todos(pagina);

            foreach (var adm in administradores)
            {
                adms.Add(
                    new AdministradorModelView
                    {
                        Id = adm.Id,
                        Email = adm.Email,
                        Perfil = adm.Perfil,
                    }
                );
            }

            return Results.Ok(adms);
        }
    )
    .RequireAuthorization()
    .WithTags("Administradores");

app.MapGet(
        "/administradores/{id}",
        ([FromRoute] int id, IAdministradorServico administradorServico) =>
        {
            var administrador = administradorServico.BuscaPorId(id);

            if (administrador == null)
                return Results.NotFound();

            return Results.Ok(
                new AdministradorModelView
                {
                    Id = administrador.Id,
                    Email = administrador.Email,
                    Perfil = administrador.Perfil,
                }
            );
        }
    )
    .RequireAuthorization()
    .WithTags("Administradores");

app.MapPost(
        "/administradores",
        (
            [FromBody] AdministradorDTO administradorDTO,
            IAdministradorServico administradorServico
        ) =>
        {
            var validacao = new ErrosDeValidacao { Mensagens = new List<string>() };

            if (string.IsNullOrEmpty(administradorDTO.Email))
                validacao.Mensagens.Add("O email do administrador é obrigatório.");

            if (string.IsNullOrEmpty(administradorDTO.Senha))
                validacao.Mensagens.Add("A senha do administrador é obrigatória.");

            if (administradorDTO.Perfil == null)
                validacao.Mensagens.Add("O perfil do administrador é obrigatório.");

            if (validacao.Mensagens.Count > 0)
                return Results.BadRequest(validacao);

            if (administradorDTO.Perfil != null)
            {
                var administrador = new Administrador
                {
                    Email = administradorDTO.Email,
                    Senha = administradorDTO.Senha,
                    Perfil = administradorDTO.Perfil.ToString() ?? "editor",
                };

                administradorServico.Incluir(administrador);

                return Results.Created(
                    $"/administrador/{administrador.Id}",
                    new AdministradorModelView
                    {
                        Id = administrador.Id,
                        Email = administrador.Email,
                        Perfil = administrador.Perfil,
                    }
                );
            }

            return Results.BadRequest("Erro ao criar administrador.");
        }
    )
    .RequireAuthorization()
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
    .RequireAuthorization()
    .WithTags("Veiculos");

app.MapGet(
        "/veiculos",
        ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
        {
            var veiculos = veiculoServico.Todos(pagina);

            return Results.Ok(veiculos);
        }
    )
    .RequireAuthorization()
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
    .RequireAuthorization()
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
    .RequireAuthorization()
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
    .RequireAuthorization()
    .WithTags("Veiculos");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
#endregion
