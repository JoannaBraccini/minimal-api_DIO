using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Dominio.Entidades;

/// <summary>
/// Entidade que representa um administrador do sistema.
/// Utilizada para autenticação e controle de acesso aos recursos da API.
/// </summary>
public class Administrador
{
    /// <summary>
    /// Identificador único do administrador (chave primária).
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;

    /// <summary>
    /// Endereço de email do administrador (usado como login).
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Senha do administrador para autenticação.
    /// </summary>
    /// <remarks>
    /// ⚠️ NOTA: Em ambiente de produção, deve ser armazenada criptografada (hash + salt).
    /// </remarks>
    [Required]
    [StringLength(50)]
    public string Senha { get; set; } = default!;

    /// <summary>
    /// Perfil de acesso do administrador (ex: "Admin", "Editor", etc.).
    /// </summary>
    [Required]
    [StringLength(10)]
    public string Perfil { get; set; } = default!;
}
