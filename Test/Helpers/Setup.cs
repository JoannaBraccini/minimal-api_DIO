using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.ModelViews;
using Test.Mocks;

namespace Test.Helpers;

public class Setup
{
    public const string PORT = "5001";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext testContext)
    {
        Setup.testContext = testContext;
        http = new WebApplicationFactory<Startup>();

        http = http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", PORT).UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
                services.AddScoped<IVeiculoServico, VeiculoServicoMock>();
            });
        });

        client = http.CreateClient();
    }

    public static void ClassCleanup()
    {
        client.Dispose();
        http.Dispose();
    }

    public static async Task<string> FazerLogin(
        string email = "adm@teste.com",
        string senha = "senha123"
    )
    {
        var loginDTO = new Administrador { Email = email, Senha = senha };

        var content = new StringContent(
            JsonSerializer.Serialize(loginDTO),
            Encoding.UTF8,
            "application/json"
        );

        var response = await client.PostAsync("/administradores/login", content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(
                result,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return admLogado?.Token ?? string.Empty;
        }

        return string.Empty;
    }
}
