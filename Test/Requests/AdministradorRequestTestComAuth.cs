using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public sealed class AdministradorRequestTestComAuth
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
    public async Task TestarBuscarAdministradorPorIdComToken()
    {
        // Arrange
        var token = await Setup.FazerLogin();
        Setup.client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await Setup.client.GetAsync("/administradores/1");

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

        // Cleanup
        Setup.client.DefaultRequestHeaders.Authorization = null;
    }

    [TestMethod]
    public async Task TestarListarAdministradoresComToken()
    {
        // Arrange
        var token = await Setup.FazerLogin();
        Setup.client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await Setup.client.GetAsync("/administradores/");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var administradores = JsonSerializer.Deserialize<List<AdministradorModelView>>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(administradores);
        Assert.IsTrue(administradores.Count > 0);

        // Cleanup
        Setup.client.DefaultRequestHeaders.Authorization = null;
    }
}
