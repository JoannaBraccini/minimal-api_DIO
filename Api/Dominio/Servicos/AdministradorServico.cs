using System.Data.Common;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

/// <summary>
/// Serviço responsável pelas operações relacionadas a administradores.
/// Implementa autenticação e outras funcionalidades administrativas.
/// </summary>
public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;

    /// <summary>
    /// Construtor que recebe o contexto do banco via injeção de dependência.
    /// </summary>
    /// <param name="contexto">Contexto do Entity Framework configurado no Program.cs</param>
    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    /// <summary>
    /// Busca um administrador específico pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID do administrador a ser buscado</param>
    /// <returns>Objeto Administrador encontrado ou null se não existir</returns>
    public Administrador? BuscaPorId(int id)
    {
        return _contexto.Administradores.Where(a => a.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Inclui um novo administrador no sistema.
    /// </summary>
    /// <param name="administrador">Entidade do administrador a ser incluída</param>
    /// <returns>O administrador criado com ID gerado pelo banco</returns>
    public Administrador Incluir(Administrador administrador)
    {
        _contexto.Administradores.Add(administrador);
        _contexto.SaveChanges();

        return administrador;
    }

    /// <summary>
    /// Realiza a autenticação de um administrador baseado em email e senha.
    /// </summary>
    /// <param name="loginDTO">DTO contendo email e senha para autenticação</param>
    /// <returns>Objeto Administrador se as credenciais forem válidas, null caso contrário</returns>
    /// <remarks>
    /// ⚠️ NOTA: Em ambiente de produção, as senhas devem ser criptografadas usando hash + salt.
    /// </remarks>
    public Administrador? Login(LoginDTO loginDTO)
    {
        var adm = _contexto
            .Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha)
            .FirstOrDefault();

        return adm;
    }

    /// <summary>
    /// Recupera uma lista paginada de administradores do sistema.
    /// </summary>
    /// <param name="pagina">Número da página para paginação (padrão: 1). Cada página contém 10 registros.</param>
    /// <returns>Lista de administradores na página especificada, limitada a 10 registros por página</returns>
    public List<Administrador> Todos(int? pagina)
    {
        var query = _contexto.Administradores.AsQueryable();

        if (pagina != null)
            return [.. query.Skip(((int)pagina - 1) * 10).Take(10)];

        return query.ToList();
    }
}
