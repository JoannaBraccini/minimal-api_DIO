using MinimalApi.Dominio.Enuns;
using MinimalApi.DTOs;

namespace Tests.Domain.DTOs;

[TestClass]
public sealed class LoginDTOTest
{
    [TestMethod]
    public void DeveCriarLoginDTOComPropriedadesValidas()
    {
        // Arrange & Act
        var loginDTO = new LoginDTO { Email = "admin@teste.com", Senha = "senha123" };

        // Assert
        Assert.IsNotNull(loginDTO);
        Assert.AreEqual("admin@teste.com", loginDTO.Email);
        Assert.AreEqual("senha123", loginDTO.Senha);
    }

    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var loginDTO = new LoginDTO();

        // Act
        loginDTO.Email = "usuario@email.com";
        loginDTO.Senha = "minhasenha";

        // Assert
        Assert.AreEqual("usuario@email.com", loginDTO.Email);
        Assert.AreEqual("minhasenha", loginDTO.Senha);
    }

    [TestMethod]
    [DataRow("", "senha123")]
    [DataRow("email@teste.com", "")]
    [DataRow("", "")]
    public void DevePermitirPropriedadesVazias(string email, string senha)
    {
        // Arrange & Act
        var loginDTO = new LoginDTO { Email = email, Senha = senha };

        // Assert
        Assert.AreEqual(email, loginDTO.Email);
        Assert.AreEqual(senha, loginDTO.Senha);
    }
}
