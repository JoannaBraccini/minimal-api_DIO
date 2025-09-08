using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

public class VeiculoServico : IVeiculoServico
{
    private readonly DbContexto _contexto;

    public VeiculoServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public void Apagar(Veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();
    }

    public void Atualizar(Veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();
    }

    public Veiculo? BuscaPorId(int id)
    {
        return _contexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
    }

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
    public List<Veiculo> Todos(int pagina = 1, string? nome = null, string? marca = null)
    {
        // Inicia uma query base para todos os veículos (ainda não executa no banco)
        var query = _contexto.Veiculos.AsQueryable();

        // Aplica filtro por nome se foi fornecido
        if (!string.IsNullOrEmpty(nome))
        {
            // EF.Functions.Like: Executa um LIKE no SQL para busca case-insensitive
            // %{nome}% significa: qualquer texto + nome + qualquer texto
            query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
        }

        // query = query.Skip((pagina - 1) * 10).Take(10);
        // return query.ToList();
        // Alternativamente, usando query syntax:
        return [.. query.Skip((pagina - 1) * 10).Take(10)];
    }
}
