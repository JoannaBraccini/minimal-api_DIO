using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace Tests.Domain.Servicos;

[TestClass]
public class AdministradorServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        // Usar o banco de teste configurado no appsettings.json
        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();
        return new DbContexto(configuration);
    }

    [TestMethod]
    public void DeveCriarAdministrador()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador
        {
            Email = "testecriar@teste.com",
            Senha = "senha123",
            Perfil = "admin",
        };

        // Act
        var administradorServico = new AdministradorServico(context);
        administradorServico.Incluir(adm);

        // Assert
        var count = context.Administradores.Count(a => a.Email == "testecriar@teste.com");
        Assert.AreEqual(1, count);
    }

    [TestMethod]
    public void DeveBuscarAdministradorPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var administradorServico = new AdministradorServico(context);
        var adm = new Administrador
        {
            Email = "testebuscaid@teste.com",
            Senha = "senha123",
            Perfil = "admin",
        };

        // Act
        administradorServico.Incluir(adm);
        var admEncontrado = administradorServico.BuscaPorId(adm.Id);

        // Assert
        Assert.IsNotNull(admEncontrado);
        Assert.AreEqual("testebuscaid@teste.com", admEncontrado.Email);
        Assert.AreEqual("admin", admEncontrado.Perfil);
    }

    [TestMethod]
    [DataRow("admin")]
    [DataRow("editor")]
    public void DeveAceitarPerfisValidos(string perfil)
    {
        // Arrange & Act
        var administrador = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "senha123",
            Perfil = perfil,
        };

        // Assert
        Assert.AreEqual(perfil, administrador.Perfil);
    }

    [TestMethod]
    public void DevePermitirAlteracaoDePropriedades()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "antigo@email.com",
            Senha = "senhaAntiga",
            Perfil = "editor",
        };

        // Act
        administrador.Email = "novo@email.com";
        administrador.Senha = "novaSenha";
        administrador.Perfil = "admin";

        // Assert
        Assert.AreEqual("novo@email.com", administrador.Email);
        Assert.AreEqual("novaSenha", administrador.Senha);
        Assert.AreEqual("admin", administrador.Perfil);
    }

    [TestMethod]
    public void DeveAutenticarAdministradorComCredenciaisValidas()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var administradorServico = new AdministradorServico(context);
        var admCriado = new Administrador
        {
            Email = "login@teste.com",
            Senha = "senha123",
            Perfil = "admin",
        };

        administradorServico.Incluir(admCriado);

        var loginDTO = new LoginDTO { Email = "login@teste.com", Senha = "senha123" };

        // Act
        var admLogado = administradorServico.Login(loginDTO);

        // Assert
        Assert.IsNotNull(admLogado);
        Assert.AreEqual("login@teste.com", admLogado.Email);
        Assert.AreEqual("admin", admLogado.Perfil);
    }

    [TestMethod]
    public void DeveRetornarNullParaCredenciaisInvalidas()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var administradorServico = new AdministradorServico(context);

        var loginDTO = new LoginDTO { Email = "inexistente@teste.com", Senha = "senhaerrada" };

        // Act
        var admLogado = administradorServico.Login(loginDTO);

        // Assert
        Assert.IsNull(admLogado);
    }

    [TestMethod]
    public void DeveListarTodosAdministradoresSemPaginacao()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var administradorServico = new AdministradorServico(context);

        // Criar múltiplos administradores
        for (int i = 1; i <= 5; i++)
        {
            administradorServico.Incluir(
                new Administrador
                {
                    Email = $"admin{i}@teste.com",
                    Senha = "senha123",
                    Perfil = "admin",
                }
            );
        }

        // Act
        var todosAdmins = administradorServico.Todos(null);

        // Assert
        Assert.AreEqual(5, todosAdmins.Count);
    }

    [TestMethod]
    public void DeveListarAdministradoresComPaginacao()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var administradorServico = new AdministradorServico(context);

        // Criar 15 administradores para testar paginação
        for (int i = 1; i <= 15; i++)
        {
            administradorServico.Incluir(
                new Administrador
                {
                    Email = $"admin{i}@teste.com",
                    Senha = "senha123",
                    Perfil = "admin",
                }
            );
        }

        // Act - primeira página (10 registros)
        var primeiraPagina = administradorServico.Todos(1);

        // Act - segunda página (5 registros restantes)
        var segundaPagina = administradorServico.Todos(2);

        // Assert
        Assert.AreEqual(10, primeiraPagina.Count);
        Assert.AreEqual(5, segundaPagina.Count);
    }

    [TestMethod]
    public void DeveRetornarNullParaIdInexistente()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var administradorServico = new AdministradorServico(context);

        // Act
        var admInexistente = administradorServico.BuscaPorId(999);

        // Assert
        Assert.IsNull(admInexistente);
    }
}
