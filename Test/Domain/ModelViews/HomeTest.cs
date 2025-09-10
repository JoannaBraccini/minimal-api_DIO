using MinimalApi.Dominio.ModelViews;

namespace Tests.Domain.ModelViews;

[TestClass]
public sealed class HomeTest
{
    [TestMethod]
    public void DeveTerMensagemPadrao()
    {
        // Arrange & Act
        var home = new Home();

        // Assert
        Assert.AreEqual("Boas vindas à API de Veículos - Minimal API", home.Mensagem);
    }

    [TestMethod]
    public void DeveTerCaminhoDocsCorreto()
    {
        // Arrange & Act
        var home = new Home();

        // Assert
        Assert.AreEqual("/swagger", home.Docs);
    }

    [TestMethod]
    public void PropriedadesDevemSerReadOnly()
    {
        // Arrange
        var home1 = new Home();
        var home2 = new Home();

        // Act & Assert - Propriedades devem retornar sempre os mesmos valores
        Assert.AreEqual(home1.Mensagem, home2.Mensagem);
        Assert.AreEqual(home1.Docs, home2.Docs);
    }
}
