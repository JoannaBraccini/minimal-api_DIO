using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Dominio.Entidades;

// Entidade que representa a tabela 'veiculos' no banco de dados
public class Veiculo
{
    // Data Annotations: Atributos que configuram como a propriedade vira coluna no banco

    [Key] // Define como chave primária (PRIMARY KEY)
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment (AUTO_INCREMENT)
    public int Id { get; set; } = default!;

    [Required] // Campo obrigatório (NOT NULL)
    [StringLength(150)] // Limite de caracteres (VARCHAR(150))
    public string Nome { get; set; } = default!;

    [Required] // Campo obrigatório (NOT NULL)
    [StringLength(100)] // Limite de caracteres (VARCHAR(100))
    public string Marca { get; set; } = default!;

    [Required] // Campo obrigatório (NOT NULL)
    public int Ano { get; set; } = default!; // INT NOT NULL
}
