using MinimalApi.Dominio.ModelViews;

namespace Test.Domain.ModelViews;

[TestClass]
public sealed class ErrosDeValidacaoTest
{
    [TestMethod]
    public void DeveCriarComListaVazia()
    {
        // Arrange & Act
        var erros = new ErrosDeValidacao { Mensagens = new List<string>() };

        // Assert
        Assert.IsNotNull(erros.Mensagens);
        Assert.AreEqual(0, erros.Mensagens.Count);
    }

    [TestMethod]
    public void DevePermitirAdicionarMensagens()
    {
        // Arrange
        var erros = new ErrosDeValidacao { Mensagens = new List<string>() };

        // Act
        erros.Mensagens.Add("Erro de validação 1");
        erros.Mensagens.Add("Erro de validação 2");

        // Assert
        Assert.AreEqual(2, erros.Mensagens.Count);
        Assert.AreEqual("Erro de validação 1", erros.Mensagens[0]);
        Assert.AreEqual("Erro de validação 2", erros.Mensagens[1]);
    }

    [TestMethod]
    public void DeveCriarComMensagensPredefinidas()
    {
        // Arrange
        var mensagens = new List<string> { "Campo obrigatório", "Formato inválido" };

        // Act
        var erros = new ErrosDeValidacao { Mensagens = mensagens };

        // Assert
        Assert.AreEqual(2, erros.Mensagens.Count);
        Assert.IsTrue(erros.Mensagens.Contains("Campo obrigatório"));
        Assert.IsTrue(erros.Mensagens.Contains("Formato inválido"));
    }

    [TestMethod]
    public void DevePermitirListaNula()
    {
        // Arrange & Act
        var erros = new ErrosDeValidacao { Mensagens = null! };

        // Assert
        Assert.IsNull(erros.Mensagens);
    }
}
