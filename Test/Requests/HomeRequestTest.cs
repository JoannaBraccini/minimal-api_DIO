using System.Net;
using System.Text.Json;
using MinimalApi.Dominio.ModelViews;
using Test.Helpers;

namespace Test.Requests;

/// <summary>
/// Testes de integração para o endpoint público da aplicação (/).
/// Verifica se a API está funcionando e retornando as informações básicas.
/// </summary>
[TestClass]
public sealed class HomeRequestTest
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
    public async Task TestarPaginaInicial()
    {
        // Arrange & Act
        var response = await Setup.client.GetAsync("/");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var home = JsonSerializer.Deserialize<Home>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.IsNotNull(home);
        Assert.IsNotNull(home.Mensagem);
        Assert.IsNotNull(home.Docs);
    }
}
