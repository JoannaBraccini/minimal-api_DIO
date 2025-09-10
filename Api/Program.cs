using MinimalApi;

/// <summary>
/// Entry point da aplicação. Configura e inicia o host web usando Startup.cs
/// para configuração dos serviços e pipeline de middleware.
/// </summary>
/// <param name="args">Argumentos da linha de comando</param>
/// <returns>Builder configurado para o host da aplicação</returns>
IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

CreateHostBuilder(args).Build().Run();
