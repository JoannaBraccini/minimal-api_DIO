using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.DTOs;

namespace Test.Mocks;

public class AdministradorServicoMock : IAdministradorServico
{
    private static readonly List<Administrador> administradores =
    [
        new Administrador
        {
            Id = 1,
            Email = "adm@teste.com",
            Senha = "senha123",
            Perfil = "admin",
        },
        new Administrador
        {
            Id = 2,
            Email = "editor@teste.com",
            Senha = "senha123",
            Perfil = "editor",
        },
    ];

    public Administrador? BuscaPorId(int id)
    {
        return administradores.Find(a => a.Id == id);
    }

    public Administrador Incluir(Administrador administrador)
    {
        administrador.Id = administradores.Count() + 1;
        administradores.Add(administrador);

        return administrador;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        return administradores.Find(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
    }

    public List<Administrador> Todos(int? pagina)
    {
        return administradores;
    }
}
