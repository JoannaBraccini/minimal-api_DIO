namespace MinimalApi.DTOs;

/// <summary>
/// Data Transfer Object (DTO) para transferência de dados de login.
/// Utilizado para deserializar JSON recebido no endpoint de autenticação.
/// </summary>
/// <example>
/// JSON esperado:
/// {
///   "email": "administrador@teste.com",
///   "senha": "senha123"
/// }
/// </example>
public class LoginDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
}
