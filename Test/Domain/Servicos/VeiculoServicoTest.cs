using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Servicos;

[TestClass]
public class VeiculoServicoTest
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
    public void DeveIncluirVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2020,
        };

        // Act
        var veiculoServico = new VeiculoServico(context);
        veiculoServico.Incluir(veiculo);

        // Assert
        Assert.IsTrue(veiculo.Id > 0);
        var veiculoSalvo = veiculoServico.BuscaPorId(veiculo.Id);
        Assert.IsNotNull(veiculoSalvo);
        Assert.AreEqual("Civic", veiculoSalvo.Nome);
    }

    [TestMethod]
    public void DeveBuscarVeiculoPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);
        var veiculo = new Veiculo
        {
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2021,
        };

        veiculoServico.Incluir(veiculo);

        // Act
        var veiculoEncontrado = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.IsNotNull(veiculoEncontrado);
        Assert.AreEqual("Corolla", veiculoEncontrado.Nome);
        Assert.AreEqual("Toyota", veiculoEncontrado.Marca);
        Assert.AreEqual(2021, veiculoEncontrado.Ano);
    }

    [TestMethod]
    public void DeveAtualizarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);
        var veiculo = new Veiculo
        {
            Nome = "Fit",
            Marca = "Honda",
            Ano = 2019,
        };

        veiculoServico.Incluir(veiculo);

        // Act
        veiculo.Nome = "Fit EX";
        veiculo.Ano = 2022;
        veiculoServico.Atualizar(veiculo);

        // Assert
        var veiculoAtualizado = veiculoServico.BuscaPorId(veiculo.Id);
        Assert.IsNotNull(veiculoAtualizado);
        Assert.AreEqual("Fit EX", veiculoAtualizado.Nome);
        Assert.AreEqual(2022, veiculoAtualizado.Ano);
    }

    [TestMethod]
    public void DeveApagarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);
        var veiculo = new Veiculo
        {
            Nome = "Gol",
            Marca = "Volkswagen",
            Ano = 2018,
        };

        veiculoServico.Incluir(veiculo);
        var idVeiculo = veiculo.Id;

        // Act
        veiculoServico.Apagar(veiculo);

        // Assert
        var veiculoApagado = veiculoServico.BuscaPorId(idVeiculo);
        Assert.IsNull(veiculoApagado);
    }

    [TestMethod]
    public void DeveListarTodosVeiculosSemPaginacao()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);

        // Criar múltiplos veículos
        for (int i = 1; i <= 5; i++)
        {
            veiculoServico.Incluir(
                new Veiculo
                {
                    Nome = $"Veiculo {i}",
                    Marca = "Marca",
                    Ano = 2020 + i,
                }
            );
        }

        // Act
        var todosVeiculos = veiculoServico.Todos(null);

        // Assert
        Assert.AreEqual(5, todosVeiculos.Count);
    }

    [TestMethod]
    public void DeveListarVeiculosComPaginacao()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);

        // Criar 15 veículos para testar paginação
        for (int i = 1; i <= 15; i++)
        {
            veiculoServico.Incluir(
                new Veiculo
                {
                    Nome = $"Veiculo {i}",
                    Marca = "Marca",
                    Ano = 2020,
                }
            );
        }

        // Act - primeira página (10 registros)
        var primeiraPagina = veiculoServico.Todos(1);

        // Act - segunda página (5 registros restantes)
        var segundaPagina = veiculoServico.Todos(2);

        // Assert
        Assert.AreEqual(10, primeiraPagina.Count);
        Assert.AreEqual(5, segundaPagina.Count);
    }

    [TestMethod]
    public void DeveFiltrarVeiculosPorNome()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);

        veiculoServico.Incluir(
            new Veiculo
            {
                Nome = "Honda Civic",
                Marca = "Honda",
                Ano = 2020,
            }
        );
        veiculoServico.Incluir(
            new Veiculo
            {
                Nome = "Toyota Corolla",
                Marca = "Toyota",
                Ano = 2021,
            }
        );
        veiculoServico.Incluir(
            new Veiculo
            {
                Nome = "Honda Fit",
                Marca = "Honda",
                Ano = 2019,
            }
        );

        // Act
        var veiculosHonda = veiculoServico.Todos(1, "Honda");

        // Assert
        Assert.AreEqual(2, veiculosHonda.Count);
        Assert.IsTrue(veiculosHonda.All(v => v.Nome.ToLower().Contains("honda")));
    }

    [TestMethod]
    public void DeveRetornarNullParaIdInexistente()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);

        // Act
        var veiculoInexistente = veiculoServico.BuscaPorId(999);

        // Assert
        Assert.IsNull(veiculoInexistente);
    }

    [TestMethod]
    public void DeveRetornarListaVaziaQuandoNaoHaVeiculos()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);

        // Act
        var veiculos = veiculoServico.Todos(1);

        // Assert
        Assert.AreEqual(0, veiculos.Count);
    }
}
