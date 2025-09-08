namespace MinimalApi.Dominio.ModelViews;

public struct Home
{
    public string Mensagem
    {
        get => "Boas vindas à API de Veículos - Minimal API";
    }
    public string Docs
    {
        get => "/swagger";
    }
}
