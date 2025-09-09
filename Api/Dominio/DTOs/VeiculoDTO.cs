namespace MinimalApi.DTOs;

/// <summary>
/// Data Transfer Object (DTO) para transferência de dados de veículo.
/// Utilizado para receber dados do cliente via JSON nos endpoints de veículos.
/// </summary>
/// <example>
/// JSON esperado:
/// {
///   "nome": "Civic",
///   "marca": "Honda",
///   "ano": 2023
/// }
/// </example>
public record VeiculoDTO
{
    public string Nome { get; set; } = default!;
    public string Marca { get; set; } = default!;
    public int Ano { get; set; } = default!;
}
