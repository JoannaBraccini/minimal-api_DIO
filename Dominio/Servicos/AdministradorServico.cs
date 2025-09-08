using System.Data.Common;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

// Serviço responsável pelas operações relacionadas a Administradores
public class AdministradorServico : IAdministradorServico
{
    // Injeção de dependência do contexto do banco de dados
    private readonly DbContexto _contexto;

    // Construtor que recebe o contexto via DI (configurado no Program.cs)
    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    // Método de autenticação: verifica email e senha no banco
    public Administrador? Login(LoginDTO loginDTO)
    {
        // LINQ para Entity Framework:
        // - Where: Filtra registros (vira WHERE no SQL)
        // - FirstOrDefault: Pega o primeiro ou null se não encontrar
        // ⚠️ NOTA: Em produção, senhas devem ser criptografadas (hash + salt)
        var adm = _contexto
            .Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha)
            .FirstOrDefault();

        return adm; // Retorna o Administrador encontrado ou null
    }
}
