using MinimalApi.Dominio.ModelViews;
using MinimalApi.DTOs;

namespace Tests.Helpers;

[TestClass]
public sealed class ValidacaoHelperTest
{
    /// <summary>
    /// Simula a validação de DTO de veículo conforme implementada no Program.cs
    /// </summary>
    private static ErrosDeValidacao ValidarVeiculoDTO(VeiculoDTO veiculoDTO)
    {
        var validacao = new ErrosDeValidacao { Mensagens = new List<string>() };

        if (string.IsNullOrEmpty(veiculoDTO.Nome))
            validacao.Mensagens.Add("O nome do veículo é obrigatório.");

        if (string.IsNullOrEmpty(veiculoDTO.Marca))
            validacao.Mensagens.Add("A marca do veículo é obrigatória.");

        if (veiculoDTO.Ano < 1886 || veiculoDTO.Ano > DateTime.Now.Year + 1)
            validacao.Mensagens.Add("O ano do veículo é inválido.");

        return validacao;
    }

    [TestMethod]
    public void ValidarVeiculoDTO_ComDadosValidos_DeveRetornarSemErros()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2023,
        };

        // Act
        var resultado = ValidarVeiculoDTO(veiculoDTO);

        // Assert
        Assert.AreEqual(0, resultado.Mensagens.Count);
    }

    [TestMethod]
    public void ValidarVeiculoDTO_ComNomeVazio_DeveRetornarErro()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "",
            Marca = "Honda",
            Ano = 2023,
        };

        // Act
        var resultado = ValidarVeiculoDTO(veiculoDTO);

        // Assert
        Assert.AreEqual(1, resultado.Mensagens.Count);
        Assert.IsTrue(resultado.Mensagens.Contains("O nome do veículo é obrigatório."));
    }

    [TestMethod]
    public void ValidarVeiculoDTO_ComMarcaVazia_DeveRetornarErro()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "",
            Ano = 2023,
        };

        // Act
        var resultado = ValidarVeiculoDTO(veiculoDTO);

        // Assert
        Assert.AreEqual(1, resultado.Mensagens.Count);
        Assert.IsTrue(resultado.Mensagens.Contains("A marca do veículo é obrigatória."));
    }

    [TestMethod]
    [DataRow(1885)] // Antes da invenção do automóvel
    [DataRow(0)]
    [DataRow(-1)]
    public void ValidarVeiculoDTO_ComAnoMuitoAntigo_DeveRetornarErro(int ano)
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = ano,
        };

        // Act
        var resultado = ValidarVeiculoDTO(veiculoDTO);

        // Assert
        Assert.AreEqual(1, resultado.Mensagens.Count);
        Assert.IsTrue(resultado.Mensagens.Contains("O ano do veículo é inválido."));
    }

    [TestMethod]
    public void ValidarVeiculoDTO_ComAnoFuturoInvalido_DeveRetornarErro()
    {
        // Arrange
        var anoInvalido = DateTime.Now.Year + 2; // Mais de 1 ano no futuro
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = anoInvalido,
        };

        // Act
        var resultado = ValidarVeiculoDTO(veiculoDTO);

        // Assert
        Assert.AreEqual(1, resultado.Mensagens.Count);
        Assert.IsTrue(resultado.Mensagens.Contains("O ano do veículo é inválido."));
    }

    [TestMethod]
    public void ValidarVeiculoDTO_ComAnoAtualEProximo_DeveSerValido()
    {
        // Arrange
        var anoAtual = DateTime.Now.Year;
        var anoProximo = DateTime.Now.Year + 1;

        var veiculoAtual = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = anoAtual,
        };

        var veiculoProximo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = anoProximo,
        };

        // Act
        var resultadoAtual = ValidarVeiculoDTO(veiculoAtual);
        var resultadoProximo = ValidarVeiculoDTO(veiculoProximo);

        // Assert
        Assert.AreEqual(0, resultadoAtual.Mensagens.Count);
        Assert.AreEqual(0, resultadoProximo.Mensagens.Count);
    }

    [TestMethod]
    public void ValidarVeiculoDTO_ComMultiplosErros_DeveRetornarTodos()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "",
            Marca = "",
            Ano = 1800,
        };

        // Act
        var resultado = ValidarVeiculoDTO(veiculoDTO);

        // Assert
        Assert.AreEqual(3, resultado.Mensagens.Count);
        Assert.IsTrue(resultado.Mensagens.Contains("O nome do veículo é obrigatório."));
        Assert.IsTrue(resultado.Mensagens.Contains("A marca do veículo é obrigatória."));
        Assert.IsTrue(resultado.Mensagens.Contains("O ano do veículo é inválido."));
    }
}
