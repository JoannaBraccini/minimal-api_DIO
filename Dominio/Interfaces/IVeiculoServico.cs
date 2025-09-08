using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Interfaces;

/// <summary>
/// Interface que define o contrato para operações de serviço relacionadas a veículos.
/// Especifica métodos para operações CRUD e consultas com filtros e paginação.
/// </summary>
public interface IVeiculoServico
{
    /// <summary>
    /// Recupera uma lista paginada de veículos com filtros opcionais.
    /// </summary>
    /// <param name="pagina">Número da página para paginação (padrão: 1)</param>
    /// <param name="nome">Filtro opcional para buscar veículos por nome</param>
    /// <param name="marca">Filtro opcional para buscar veículos por marca</param>
    /// <returns>Lista de veículos que atendem aos critérios especificados</returns>
    List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null);

    /// <summary>
    /// Busca um veículo específico pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID do veículo a ser buscado</param>
    /// <returns>Objeto Veículo encontrado ou null se não existir</returns>
    Veiculo? BuscaPorId(int id);

    /// <summary>
    /// Adiciona um novo veículo ao sistema.
    /// </summary>
    /// <param name="veiculo">Entidade do veículo a ser incluída</param>
    void Incluir(Veiculo veiculo);

    /// <summary>
    /// Atualiza os dados de um veículo existente.
    /// </summary>
    /// <param name="veiculo">Entidade do veículo com os dados atualizados</param>
    void Atualizar(Veiculo veiculo);

    /// <summary>
    /// Remove um veículo do sistema.
    /// </summary>
    /// <param name="veiculo">Entidade do veículo a ser removida</param>
    void Apagar(Veiculo veiculo);
}
