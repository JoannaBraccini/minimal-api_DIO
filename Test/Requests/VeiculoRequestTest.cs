using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

/// <summary>
/// Testes de integração para os endpoints da API relacionados aos veículos.
/// Testa funcionalidades CRUD completas com autenticação JWT.
/// </summary>
[TestClass]
public sealed class VeiculoRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    /// <summary>
    /// Testa a criação de um novo veículo com dados válidos usando autenticação admin.
    /// Verifica se o endpoint POST /veiculos retorna status Created e os dados corretos.
    /// </summary>
    [TestMethod]
    public async Task TestarCriarVeiculo()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2020,
        };

        var content = new StringContent(
            JsonSerializer.Serialize(veiculoDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act - usando autenticação com perfil admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.PostAsync("/veiculos", content)
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var veiculo = JsonSerializer.Deserialize<Veiculo>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(veiculo);
        Assert.AreEqual("Civic", veiculo.Nome);
        Assert.AreEqual("Honda", veiculo.Marca);
        Assert.AreEqual(2020, veiculo.Ano);
    }

    /// <summary>
    /// Testa a criação de veículo com dados inválidos (nome/marca vazios, ano inválido).
    /// Verifica se o endpoint retorna BadRequest e as validações funcionam corretamente.
    /// </summary>
    [TestMethod]
    public async Task TestarCriarVeiculoComDadosInvalidos()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "", // Nome vazio deve gerar erro
            Marca = "",
            Ano = 1800, // Ano inválido
        };

        var content = new StringContent(
            JsonSerializer.Serialize(veiculoDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act - usando autenticação com perfil admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.PostAsync("/veiculos", content)
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarListarVeiculos()
    {
        // Arrange & Act - usando autenticação
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.GetAsync("/veiculos")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var veiculos = JsonSerializer.Deserialize<List<Veiculo>>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(veiculos);
        Assert.IsTrue(veiculos.Count > 0);
    }

    [TestMethod]
    public async Task TestarBuscarVeiculoPorId()
    {
        // Arrange & Act - usando autenticação
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.GetAsync("/veiculos/1")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var veiculo = JsonSerializer.Deserialize<Veiculo>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(veiculo);
        Assert.IsTrue(veiculo.Id > 0);
    }

    [TestMethod]
    public async Task TestarBuscarVeiculoInexistente()
    {
        // Arrange & Act - usando autenticação
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.GetAsync("/veiculos/999")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarAtualizarVeiculo()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic Atualizado",
            Marca = "Honda",
            Ano = 2022,
        };

        var content = new StringContent(
            JsonSerializer.Serialize(veiculoDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.PutAsync("/veiculos/1", content)
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var veiculo = JsonSerializer.Deserialize<Veiculo>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(veiculo);
        Assert.AreEqual("Civic Atualizado", veiculo.Nome);
        Assert.AreEqual(2022, veiculo.Ano);
    }

    [TestMethod]
    public async Task TestarAtualizarVeiculoInexistente()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2020,
        };

        var content = new StringContent(
            JsonSerializer.Serialize(veiculoDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.PutAsync("/veiculos/999", content)
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarDeletarVeiculo()
    {
        // Arrange & Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.DeleteAsync("/veiculos/1")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarDeletarVeiculoInexistente()
    {
        // Arrange & Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.DeleteAsync("/veiculos/999")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
