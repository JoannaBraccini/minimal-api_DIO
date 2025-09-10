using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;

namespace Test.Mocks;

public class VeiculoServicoMock : IVeiculoServico
{
    private static readonly List<Veiculo> veiculos =
    [
        new Veiculo
        {
            Id = 1,
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2020,
        },
        new Veiculo
        {
            Id = 2,
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2021,
        },
        new Veiculo
        {
            Id = 3,
            Nome = "Gol",
            Marca = "Volkswagen",
            Ano = 2019,
        },
    ];

    public void Apagar(Veiculo veiculo)
    {
        veiculos.Remove(veiculo);
    }

    public void Atualizar(Veiculo veiculo)
    {
        var veiculoExistente = veiculos.Find(v => v.Id == veiculo.Id);
        if (veiculoExistente != null)
        {
            veiculoExistente.Nome = veiculo.Nome;
            veiculoExistente.Marca = veiculo.Marca;
            veiculoExistente.Ano = veiculo.Ano;
        }
    }

    public Veiculo? BuscaPorId(int id)
    {
        return veiculos.Find(v => v.Id == id);
    }

    public void Incluir(Veiculo veiculo)
    {
        veiculo.Id = veiculos.Count + 1;
        veiculos.Add(veiculo);
    }

    public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
    {
        var query = veiculos.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(v => v.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(marca))
            query = query.Where(v => v.Marca.Contains(marca, StringComparison.OrdinalIgnoreCase));

        if (pagina != null)
            return query.Skip(((int)pagina - 1) * 10).Take(10).ToList();

        return query.ToList();
    }
}
