using MinimalApi.Dominio.ModelViews;

namespace Test.Domain.ModelViews;

[TestClass]
public sealed class AdministradorModelViewTest
{
    [TestMethod]
    public void DeveCriarAdministradorModelViewComPropriedadesValidas()
    {
        // Arrange & Act
        var adminModelView = new AdministradorModelView
        {
            Id = 1,
            Email = "admin@sistema.com",
            Perfil = "admin",
        };

        // Assert
        Assert.IsNotNull(adminModelView);
        Assert.AreEqual(1, adminModelView.Id);
        Assert.AreEqual("admin@sistema.com", adminModelView.Email);
        Assert.AreEqual("admin", adminModelView.Perfil);
    }

    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adminModelView = new AdministradorModelView();

        // Act
        adminModelView.Id = 99;
        adminModelView.Email = "editor@teste.com";
        adminModelView.Perfil = "editor";

        // Assert
        Assert.AreEqual(99, adminModelView.Id);
        Assert.AreEqual("editor@teste.com", adminModelView.Email);
        Assert.AreEqual("editor", adminModelView.Perfil);
    }

    [TestMethod]
    [DataRow(1, "admin@teste.com", "admin")]
    [DataRow(2, "editor@sistema.com", "editor")]
    [DataRow(100, "usuario@email.com", "admin")]
    public void DeveAceitarDiferentesCombinacoes(int id, string email, string perfil)
    {
        // Arrange & Act
        var adminModelView = new AdministradorModelView
        {
            Id = id,
            Email = email,
            Perfil = perfil,
        };

        // Assert
        Assert.AreEqual(id, adminModelView.Id);
        Assert.AreEqual(email, adminModelView.Email);
        Assert.AreEqual(perfil, adminModelView.Perfil);
    }
}
