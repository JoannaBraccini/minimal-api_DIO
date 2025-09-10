using MinimalApi.Dominio.Entidades;

namespace Tests.Domain.Entidades;

[TestClass]
public sealed class AdministradorTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adm = new Administrador();

        // Act
        adm.Id = 1;
        adm.Email = "teste@teste.com";
        adm.Senha = "senhateste";
        adm.Perfil = "admin";

        // Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("teste@teste.com", adm.Email);
        Assert.AreEqual("senhateste", adm.Senha);
        Assert.AreEqual("admin", adm.Perfil);
    }

    [TestMethod]
    public void DeveCriarAdministradorComPropriedadesValidas()
    {
        // Arrange & Act
        var administrador = new Administrador
        {
            Id = 2,
            Email = "admin@sistema.com",
            Senha = "senha123",
            Perfil = "admin",
        };

        // Assert
        Assert.IsNotNull(administrador);
        Assert.AreEqual(2, administrador.Id);
        Assert.AreEqual("admin@sistema.com", administrador.Email);
        Assert.AreEqual("senha123", administrador.Senha);
        Assert.AreEqual("admin", administrador.Perfil);
    }

    [TestMethod]
    [DataRow("admin")]
    [DataRow("editor")]
    public void DeveAceitarPerfisValidos(string perfil)
    {
        // Arrange & Act
        var administrador = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "senha123",
            Perfil = perfil,
        };

        // Assert
        Assert.AreEqual(perfil, administrador.Perfil);
    }

    [TestMethod]
    public void DevePermitirAlteracaoDePropriedades()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "antigo@email.com",
            Senha = "senhaAntiga",
            Perfil = "editor",
        };

        // Act
        administrador.Email = "novo@email.com";
        administrador.Senha = "novaSenha";
        administrador.Perfil = "admin";

        // Assert
        Assert.AreEqual("novo@email.com", administrador.Email);
        Assert.AreEqual("novaSenha", administrador.Senha);
        Assert.AreEqual("admin", administrador.Perfil);
    }
}
