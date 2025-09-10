using MinimalApi.Dominio.Enuns;

namespace Test.Domain.Enuns;

[TestClass]
public sealed class PerfilTest
{
    [TestMethod]
    public void DeveConterDoisValores()
    {
        // Arrange & Act
        var valores = Enum.GetValues<Perfil>();

        // Assert
        Assert.AreEqual(2, valores.Length);
    }

    [TestMethod]
    public void DeveConterValorAdmin()
    {
        // Arrange & Act
        var contemAdmin = Enum.IsDefined(typeof(Perfil), Perfil.admin);

        // Assert
        Assert.IsTrue(contemAdmin);
    }

    [TestMethod]
    public void DeveConterValorEditor()
    {
        // Arrange & Act
        var contemEditor = Enum.IsDefined(typeof(Perfil), Perfil.editor);

        // Assert
        Assert.IsTrue(contemEditor);
    }

    [TestMethod]
    public void DeveConverterParaString()
    {
        // Arrange & Act
        var adminString = Perfil.admin.ToString();
        var editorString = Perfil.editor.ToString();

        // Assert
        Assert.AreEqual("admin", adminString);
        Assert.AreEqual("editor", editorString);
    }

    [TestMethod]
    public void DeveConverterDeString()
    {
        // Arrange & Act
        var adminEnum = Enum.Parse<Perfil>("admin");
        var editorEnum = Enum.Parse<Perfil>("editor");

        // Assert
        Assert.AreEqual(Perfil.admin, adminEnum);
        Assert.AreEqual(Perfil.editor, editorEnum);
    }

    [TestMethod]
    public void DeveTerValoresNumericos()
    {
        // Arrange & Act
        var adminValue = (int)Perfil.admin;
        var editorValue = (int)Perfil.editor;

        // Assert
        Assert.AreEqual(0, adminValue);
        Assert.AreEqual(1, editorValue);
    }

    [TestMethod]
    public void DeveRetornarErroParaValorInvalido()
    {
        // Arrange, Act & Assert
        Assert.ThrowsExactly<ArgumentException>(() => Enum.Parse<Perfil>("invalido"));
    }
}
