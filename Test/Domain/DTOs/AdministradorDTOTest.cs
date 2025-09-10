using MinimalApi.Dominio.Enuns;
using MinimalApi.DTOs;

namespace Test.Domain.DTOs;

[TestClass]
public sealed class AdministradorDTOTest
{
    [TestMethod]
    public void DeveCriarAdministradorDTOComPropriedadesValidas()
    {
        // Arrange & Act
        var adminDTO = new AdministradorDTO
        {
            Email = "admin@sistema.com",
            Senha = "senha123",
            Perfil = Perfil.admin,
        };

        // Assert
        Assert.IsNotNull(adminDTO);
        Assert.AreEqual("admin@sistema.com", adminDTO.Email);
        Assert.AreEqual("senha123", adminDTO.Senha);
        Assert.AreEqual(Perfil.admin, adminDTO.Perfil);
    }

    [TestMethod]
    [DataRow(Perfil.admin)]
    [DataRow(Perfil.editor)]
    public void DeveAceitarTodosPerfisValidos(Perfil perfil)
    {
        // Arrange & Act
        var adminDTO = new AdministradorDTO
        {
            Email = "teste@teste.com",
            Senha = "senha123",
            Perfil = perfil,
        };

        // Assert
        Assert.AreEqual(perfil, adminDTO.Perfil);
    }

    [TestMethod]
    public void DevePermitirPerfilNulo()
    {
        // Arrange & Act
        var adminDTO = new AdministradorDTO
        {
            Email = "teste@teste.com",
            Senha = "senha123",
            Perfil = null,
        };

        // Assert
        Assert.IsNull(adminDTO.Perfil);
    }

    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adminDTO = new AdministradorDTO();

        // Act
        adminDTO.Email = "novo@email.com";
        adminDTO.Senha = "novaSenha";
        adminDTO.Perfil = Perfil.editor;

        // Assert
        Assert.AreEqual("novo@email.com", adminDTO.Email);
        Assert.AreEqual("novaSenha", adminDTO.Senha);
        Assert.AreEqual(Perfil.editor, adminDTO.Perfil);
    }
}
