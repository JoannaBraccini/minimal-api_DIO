using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db;

// Contexto do Entity Framework - representa a sessão com o banco de dados
// Primary Constructor (C# 12): recebe IConfiguration direto no construtor
public class DbContexto(IConfiguration configuracaoAppSettings) : DbContext
{
    // Campo readonly para acessar configurações do appsettings.json
    private readonly IConfiguration _configuracaoAppSettings = configuracaoAppSettings;

    // DbSet: Representa uma tabela no banco - permite operações CRUD
    // Cada DbSet vira uma tabela: Administradores e Veiculos
    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    // Configuração do modelo de dados e seed data (dados iniciais)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // HasData: Insere dados iniciais na tabela quando a migration é aplicada
        // Útil para ter um usuário admin padrão para testes
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                Id = 1,
                Email = "administrador@teste.com",
                Senha = "senha123",
                Perfil = "Admin"
            });
    }
    
    // Configuração da conexão com o banco de dados
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Busca a string de conexão no appsettings.json (chave: "mysql")
        var stringConexao = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();

        // Se encontrou a string de conexão, configura o MySQL com Pomelo
        if (!string.IsNullOrEmpty(stringConexao))
            optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
    }
}