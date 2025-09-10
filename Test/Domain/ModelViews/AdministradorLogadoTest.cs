using MinimalApi.Dominio.ModelViews;

namespace Test.Domain.ModelViews;

[TestClass]
public sealed class AdministradorLogadoTest
{
    [TestMethod]
    public void DeveCriarAdministradorLogadoComPropriedadesValidas()
    {
        // Arrange & Act
        var adminLogado = new AdministradorLogado
        {
            Email = "admin@teste.com",
            Perfil = "admin",
            Token = "jwt.token.aqui",
        };

        // Assert
        Assert.IsNotNull(adminLogado);
        Assert.AreEqual("admin@teste.com", adminLogado.Email);
        Assert.AreEqual("admin", adminLogado.Perfil);
        Assert.AreEqual("jwt.token.aqui", adminLogado.Token);
    }

    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adminLogado = new AdministradorLogado();

        // Act
        adminLogado.Email = "editor@sistema.com";
        adminLogado.Perfil = "editor";
        adminLogado.Token = "novo.jwt.token";

        // Assert
        Assert.AreEqual("editor@sistema.com", adminLogado.Email);
        Assert.AreEqual("editor", adminLogado.Perfil);
        Assert.AreEqual("novo.jwt.token", adminLogado.Token);
    }

    [TestMethod]
    [DataRow("admin")]
    [DataRow("editor")]
    public void DeveAceitarDiferentesPerfis(string perfil)
    {
        // Arrange & Act
        var adminLogado = new AdministradorLogado
        {
            Email = "teste@teste.com",
            Perfil = perfil,
            Token = "token.jwt",
        };

        // Assert
        Assert.AreEqual(perfil, adminLogado.Perfil);
    }
}
