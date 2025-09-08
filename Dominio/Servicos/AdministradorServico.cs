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
}
