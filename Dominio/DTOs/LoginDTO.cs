namespace MinimalApi.DTOs;

// DTO (Data Transfer Object): Classe para transferir dados entre camadas
// Usado para receber dados do cliente via JSON no endpoint POST /login
public class LoginDTO
{
    // Propriedades que serão deserializadas do JSON do corpo da requisição
    // Exemplo JSON: { "email": "admin@teste.com", "senha": "123456" }

    public string Email { get; set; } = default!; // Campo email do login
    public string Senha { get; set; } = default!; // Campo senha do login

    // default!: Suprime warnings de nullability do C#
    // (informa ao compilador que será inicializado via JSON)
}
