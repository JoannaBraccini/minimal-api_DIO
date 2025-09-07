namespace MinimalApi.DTOs;

public class LoginDTO
{
    // Usar default! para evitar warnings de nullability
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
}
