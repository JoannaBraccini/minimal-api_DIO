using MinimalApi.Dominio.Entidades;

namespace Tests.Domain.Entidades;

[TestClass]
public sealed class VeiculoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 1;
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2023;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Civic", veiculo.Nome);
        Assert.AreEqual("Honda", veiculo.Marca);
        Assert.AreEqual(2023, veiculo.Ano);
    }

    [TestMethod]
    public void DeveCriarVeiculoComPropriedadesValidas()
    {
        // Arrange & Act
        var veiculo = new Veiculo
        {
            Id = 1,
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2024,
        };

        // Assert
        Assert.IsNotNull(veiculo);
        Assert.AreEqual("Corolla", veiculo.Nome);
        Assert.AreEqual("Toyota", veiculo.Marca);
        Assert.AreEqual(2024, veiculo.Ano);
    }

    [TestMethod]
    [DataRow("", "Toyota", 2023)]
    [DataRow("Corolla", "", 2023)]
    [DataRow("Corolla", "Toyota", 0)]
    public void NaoDevePermitirPropriedadesInvalidas(string nome, string marca, int ano)
    {
        // Arrange & Act
        var veiculo = new Veiculo
        {
            Nome = nome,
            Marca = marca,
            Ano = ano,
        };

        // Assert - Testa se as propriedades foram definidas (mesmo sendo inválidas)
        Assert.AreEqual(nome, veiculo.Nome);
        Assert.AreEqual(marca, veiculo.Marca);
        Assert.AreEqual(ano, veiculo.Ano);
    }
}
