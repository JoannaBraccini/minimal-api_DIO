var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost(
    "/login",
    (MinimalApi.DTOs.LoginDTO loginDTO) =>
    {
        if (loginDTO.Email == "admin@teste.com" && loginDTO.Senha == "senha123")
            return Results.Ok("Login com sucesso!");
        else
            return Results.Unauthorized();
    }
);

app.Run();
