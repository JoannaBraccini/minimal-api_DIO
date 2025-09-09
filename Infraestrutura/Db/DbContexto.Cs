using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;

namespace MinimalApi.Infraestrutura.Db;

/// <summary>
/// Contexto do Entity Framework Core que representa a sessão com o banco de dados MySQL.
/// Configurado com Primary Constructor (C# 12) para injeção de dependência automática.
/// </summary>
public class DbContexto(IConfiguration configuracaoAppSettings) : DbContext
{
    private readonly IConfiguration _configuracaoAppSettings = configuracaoAppSettings;

    /// <summary>
    /// Representa a tabela 'administradores' no banco de dados.
    /// </summary>
    public DbSet<Administrador> Administradores { get; set; } = default!;
    
    /// <summary>
    /// Representa a tabela 'veiculos' no banco de dados.
    /// </summary>
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    /// <summary>
    /// Configura o modelo de dados e insere dados iniciais (seed data).
    /// </summary>
    /// <param name="modelBuilder">Builder para configuração do modelo</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                Id = 1,
                Email = "administrador@teste.com",
                Senha = "senha123",
                Perfil = "admin"
            });

        modelBuilder.Entity<Veiculo>().HasData(
            new Veiculo
            {
                Id = 1,
                Nome = "Civic",
                Marca = "Honda",
                Ano = 2023
            },
            new Veiculo
            {
                Id = 2,
                Nome = "Corolla",
                Marca = "Toyota",
                Ano = 2024
            },
            new Veiculo
            {
                Id = 3,
                Nome = "Golf",
                Marca = "Volkswagen",
                Ano = 2022
            },
            new Veiculo
            {
                Id = 4,
                Nome = "HB20",
                Marca = "Hyundai",
                Ano = 2023
            },
            new Veiculo
            {
                Id = 5,
                Nome = "Onix",
                Marca = "Chevrolet",
                Ano = 2024
            });
    }
    
    /// <summary>
    /// Configura a conexão com o banco MySQL usando a string de conexão do appsettings.json.
    /// </summary>
    /// <param name="optionsBuilder">Builder para configurações do contexto</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var stringConexao = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();

        if (!string.IsNullOrEmpty(stringConexao))
            optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
    }
}