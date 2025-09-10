using MinimalApi.DTOs;

namespace Tests.Domain.DTOs;

[TestClass]
public sealed class VeiculoDTOTest
{
    [TestMethod]
    public void DeveCriarVeiculoDTOComPropriedadesValidas()
    {
        // Arrange & Act
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2023,
        };

        // Assert
        Assert.IsNotNull(veiculoDTO);
        Assert.AreEqual("Civic", veiculoDTO.Nome);
        Assert.AreEqual("Honda", veiculoDTO.Marca);
        Assert.AreEqual(2023, veiculoDTO.Ano);
    }

    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO();

        // Act
        veiculoDTO.Nome = "Corolla";
        veiculoDTO.Marca = "Toyota";
        veiculoDTO.Ano = 2024;

        // Assert
        Assert.AreEqual("Corolla", veiculoDTO.Nome);
        Assert.AreEqual("Toyota", veiculoDTO.Marca);
        Assert.AreEqual(2024, veiculoDTO.Ano);
    }

    [TestMethod]
    [DataRow("Civic", "Honda", 2023)]
    [DataRow("Corolla", "Toyota", 2024)]
    [DataRow("Golf", "Volkswagen", 2022)]
    public void DeveAceitarDiferentesCombinacoes(string nome, string marca, int ano)
    {
        // Arrange & Act
        var veiculoDTO = new VeiculoDTO
        {
            Nome = nome,
            Marca = marca,
            Ano = ano,
        };

        // Assert
        Assert.AreEqual(nome, veiculoDTO.Nome);
        Assert.AreEqual(marca, veiculoDTO.Marca);
        Assert.AreEqual(ano, veiculoDTO.Ano);
    }

    [TestMethod]
    [DataRow("", "Honda", 2023)]
    [DataRow("Civic", "", 2023)]
    [DataRow("Civic", "Honda", 0)]
    public void DevePermitirValoresInvalidos(string nome, string marca, int ano)
    {
        // Arrange & Act
        var veiculoDTO = new VeiculoDTO
        {
            Nome = nome,
            Marca = marca,
            Ano = ano,
        };

        // Assert - DTO não valida, apenas transporta dados
        Assert.AreEqual(nome, veiculoDTO.Nome);
        Assert.AreEqual(marca, veiculoDTO.Marca);
        Assert.AreEqual(ano, veiculoDTO.Ano);
    }
}
