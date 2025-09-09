namespace MinimalApi.Dominio.ModelViews;

/// <summary>
/// Model View que representa a resposta do endpoint raiz da API.
/// Fornece informações básicas sobre a API e documentação disponível.
/// </summary>
public struct Home
{
    /// <summary>
    /// Mensagem de boas-vindas da API.
    /// </summary>
    public string Mensagem
    {
        get => "Boas vindas à API de Veículos - Minimal API";
    }

    /// <summary>
    /// Caminho para a documentação Swagger da API.
    /// </summary>
    public string Docs
    {
        get => "/swagger";
    }
}
