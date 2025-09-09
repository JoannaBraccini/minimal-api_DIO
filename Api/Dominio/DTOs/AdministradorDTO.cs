using MinimalApi.Dominio.Enuns;

namespace MinimalApi.DTOs;

/// <summary>
/// Data Transfer Object (DTO) para transferência de dados de administrador.
/// Utilizado para receber dados do cliente via JSON nos endpoints de administradores.
/// </summary>
/// <example>
/// JSON esperado:
/// {
///   "email": "administrador@teste.com",
///   "senha": "senha123",
///   "perfil": "admin"
/// }
/// </example>
public class AdministradorDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
    public Perfil? Perfil { get; set; } = default!;
}
