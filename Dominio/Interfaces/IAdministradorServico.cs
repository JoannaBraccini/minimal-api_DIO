using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Interfaces;

/// <summary>
/// Interface que define o contrato para operações de serviço relacionadas a administradores.
/// Especifica métodos para autenticação e gerenciamento de administradores do sistema.
/// </summary>
public interface IAdministradorServico
{
    /// <summary>
    /// Realiza a autenticação de um administrador baseado em credenciais.
    /// </summary>
    /// <param name="loginDTO">DTO contendo email e senha para autenticação</param>
    /// <returns>Objeto Administrador se as credenciais forem válidas, null caso contrário</returns>
    Administrador? Login(LoginDTO loginDTO);

    /// <summary>
    /// Inclui um novo administrador no sistema.
    /// </summary>
    /// <param name="administrador">Entidade do administrador a ser incluída</param>
    /// <returns>O administrador criado com ID gerado</returns>
    Administrador Incluir(Administrador administrador);

    /// <summary>
    /// Busca um administrador específico pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID do administrador a ser buscado</param>
    /// <returns>Objeto Administrador encontrado ou null se não existir</returns>
    Administrador? BuscaPorId(int id);

    /// <summary>
    /// Recupera uma lista paginada de administradores.
    /// </summary>
    /// <param name="pagina">Número da página para paginação (padrão: 1)</param>
    /// <returns>Lista de administradores na página especificada</returns>
    List<Administrador> Todos(int? pagina);
}
