using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Dominio.ModelViews;

namespace Test.Helpers;

/// <summary>
/// Helper para validação e manipulação de tokens JWT nos testes
/// </summary>
public static class JwtValidacaoHelper
{
    /// <summary>
    /// Adiciona o token JWT ao cabeçalho Authorization do HttpClient
    /// </summary>
    public static void AdicionarTokenAoCliente(HttpClient client, string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }
    }

    /// <summary>
    /// Remove o token JWT do cabeçalho Authorization do HttpClient
    /// </summary>
    public static void RemoverTokenDoCliente(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = null;
    }

    /// <summary>
    /// Faz login e retorna um token válido com o perfil especificado
    /// </summary>
    public static async Task<string> ObterTokenComPerfil(HttpClient client, Perfil perfil)
    {
        var email = perfil == Perfil.admin ? "adm@teste.com" : "editor@teste.com";
        var senha = "senha123";

        var loginData = new { Email = email, Senha = senha };

        var content = new StringContent(
            JsonSerializer.Serialize(loginData),
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

    /// <summary>
    /// Valida se a resposta HTTP contém erro de autenticação/autorização
    /// </summary>
    public static void ValidarRespostaAutenticacao(
        HttpResponseMessage response,
        bool deveEstarAutenticado = true
    )
    {
        if (deveEstarAutenticado)
        {
            Assert.AreNotEqual(
                System.Net.HttpStatusCode.Unauthorized,
                response.StatusCode,
                "A resposta não deveria retornar Unauthorized quando autenticado"
            );

            Assert.AreNotEqual(
                System.Net.HttpStatusCode.Forbidden,
                response.StatusCode,
                "A resposta não deveria retornar Forbidden quando o usuário tem permissão"
            );
        }
        else
        {
            Assert.IsTrue(
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized
                    || response.StatusCode == System.Net.HttpStatusCode.Forbidden,
                "A resposta deveria retornar Unauthorized ou Forbidden quando não autenticado"
            );
        }
    }

    /// <summary>
    /// Executa uma ação com token JWT temporário
    /// </summary>
    public static async Task<T> ComTokenTemporario<T>(
        HttpClient client,
        Perfil perfil,
        Func<Task<T>> acao
    )
    {
        var token = await ObterTokenComPerfil(client, perfil);

        try
        {
            AdicionarTokenAoCliente(client, token);
            return await acao();
        }
        finally
        {
            RemoverTokenDoCliente(client);
        }
    }

    /// <summary>
    /// Executa uma ação com token JWT temporário (sem retorno)
    /// </summary>
    public static async Task ComTokenTemporario(HttpClient client, Perfil perfil, Func<Task> acao)
    {
        var token = await ObterTokenComPerfil(client, perfil);

        try
        {
            AdicionarTokenAoCliente(client, token);
            await acao();
        }
        finally
        {
            RemoverTokenDoCliente(client);
        }
    }
}
