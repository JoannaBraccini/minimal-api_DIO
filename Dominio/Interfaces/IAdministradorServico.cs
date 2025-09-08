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
}
