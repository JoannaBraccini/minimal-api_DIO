using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

/// <summary>
/// Testes de integração para os endpoints da API relacionados aos administradores.
/// Inclui testes de autenticação, criação, busca e validação de permissões.
/// </summary>
[TestClass]
public sealed class AdministradorRequestTest
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

    [TestMethod]
    public async Task TestarGetSetPropriedades()
    {
        // Arrange
        var loginDTO = new Administrador
        {
            Id = 1,
            Email = "adm@teste.com",
            Senha = "senha123",
        };

        var content = new StringContent(
            JsonSerializer.Serialize(loginDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(admLogado?.Email ?? "");
        Assert.IsNotNull(admLogado?.Perfil ?? "");
        Assert.IsNotNull(admLogado?.Token ?? "");
    }

    [TestMethod]
    public async Task TestarLoginComCredenciaisInvalidas()
    {
        // Arrange
        var loginDTO = new Administrador { Email = "invalido@teste.com", Senha = "senhaerrada" };

        var content = new StringContent(
            JsonSerializer.Serialize(loginDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarBuscarAdministradorPorId()
    {
        // Arrange & Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.GetAsync("/administradores/1")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var administrador = JsonSerializer.Deserialize<AdministradorModelView>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(administrador);
        Assert.AreEqual("adm@teste.com", administrador.Email);
        Assert.AreEqual("admin", administrador.Perfil);
    }

    [TestMethod]
    public async Task TestarBuscarAdministradorInexistente()
    {
        // Arrange & Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.GetAsync("/administradores/999")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarListarAdministradores()
    {
        // Arrange & Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.GetAsync("/administradores/")
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var administradores = JsonSerializer.Deserialize<List<AdministradorModelView>>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(administradores);
        Assert.IsTrue(administradores.Count > 0);
    }

    [TestMethod]
    public async Task TestarCriarAdministrador()
    {
        // Arrange
        var administradorDTO = new AdministradorDTO
        {
            Email = "novo@teste.com",
            Senha = "senha123",
            Perfil = MinimalApi.Dominio.Enuns.Perfil.admin,
        };

        var content = new StringContent(
            JsonSerializer.Serialize(administradorDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.PostAsync("/administradores", content)
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var administrador = JsonSerializer.Deserialize<AdministradorModelView>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(administrador);
        Assert.AreEqual("novo@teste.com", administrador.Email);
        Assert.AreEqual("admin", administrador.Perfil);
    }

    [TestMethod]
    public async Task TestarCriarAdministradorComDadosInvalidos()
    {
        // Arrange
        var administradorDTO = new AdministradorDTO
        {
            Email = "", // Email vazio deve gerar erro
            Senha = "",
            Perfil = null,
        };

        var content = new StringContent(
            JsonSerializer.Serialize(administradorDTO),
            Encoding.UTF8,
            "application/json"
        );

        // Act - usando autenticação admin
        var response = await JwtValidacaoHelper.ComTokenTemporario(
            Setup.client,
            Perfil.admin,
            async () => await Setup.client.PostAsync("/administradores", content)
        );

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
