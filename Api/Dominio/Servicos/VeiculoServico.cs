using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

/// <summary>
/// Serviço responsável pelas operações CRUD de veículos no banco de dados.
/// Implementa padrões de repository e utiliza Entity Framework Core para acesso aos dados.
/// </summary>
public class VeiculoServico : IVeiculoServico
{
    private readonly DbContexto _contexto;

    /// <summary>
    /// Construtor que recebe o contexto do banco via injeção de dependência.
    /// </summary>
    /// <param name="contexto">Contexto do Entity Framework configurado no Program.cs</param>
    public VeiculoServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    /// <summary>
    /// Remove um veículo do banco de dados.
    /// </summary>
    /// <param name="veiculo">Entidade do veículo a ser removida</param>
    public void Apagar(Veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();
    }

    /// <summary>
    /// Atualiza os dados de um veículo existente no banco.
    /// </summary>
    /// <param name="veiculo">Entidade do veículo com os dados atualizados</param>
    public void Atualizar(Veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();
    }

    /// <summary>
    /// Busca um veículo específico pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID do veículo a ser buscado</param>
    /// <returns>Objeto Veículo encontrado ou null se não existir</returns>
    public Veiculo? BuscaPorId(int id)
    {
        return _contexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Adiciona um novo veículo ao banco de dados.
    /// </summary>
    /// <param name="veiculo">Entidade do veículo a ser incluída</param>
    public void Incluir(Veiculo veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();
    }

    /// <summary>
    /// Recupera uma lista paginada de veículos com filtros opcionais.
    /// </summary>
    /// <param name="pagina">Número da página para paginação (padrão: 1). Cada página contém 10 registros.</param>
    /// <param name="nome">Filtro opcional para buscar veículos cujo nome contenha o texto especificado (busca case-insensitive).</param>
    /// <param name="marca">Parâmetro de filtro por marca (não implementado na versão atual).</param>
    /// <returns>Lista de veículos que atendem aos critérios de filtro, limitada a 10 registros por página.</returns>
    /// <remarks>
    /// A busca por nome utiliza LIKE do SQL para correspondência parcial e é case-insensitive.
    /// O parâmetro marca está declarado mas não é utilizado na implementação atual.
    /// </remarks>
    public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
    {
        var query = _contexto.Veiculos.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));

        // query = query.Skip((pagina - 1) * 10).Take(10);
        // return query.ToList();
        // Alternativamente, usando query syntax:
        if (pagina != null)
            return [.. query.Skip(((int)pagina - 1) * 10).Take(10)];

        return query.ToList();
    }
}
