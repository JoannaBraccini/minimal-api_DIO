# 🚀 Minimal API - Projeto .NET

<div align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 9.0" />
  <img src="https://img.shields.io/badge/Entity_Framework-Core-512BD4?style=for-the-badge&logo=microsoft&logoColor=white" alt="Entity Framework Core" />
  <img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white" alt="MySQL" />
  <img src="https://img.shields.io/badge/JWT-Bearer-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" alt="JWT Bearer" />
  <img src="https://img.shields.io/badge/DIO-Bootcamp-FF6B35?style=for-the-badge&logo=graduation-cap&logoColor=white" alt="DIO Bootcamp" />
</div>

<br>

> 🎯 **Projeto desenvolvido durante o Bootcamp da DIO (Digital Innovation One)**  
> Uma API minimalista e eficiente construída com as melhores práticas do .NET moderno.

---

## � Funcionalidades

### 🔐 **Autenticação de Administradores**

- Login com email e senha retornando token JWT
- Sistema de perfis (admin/editor) usando enums
- Autenticação Bearer Token para endpoints protegidos
- Tokens com expiração de 24 horas
- Validações de entrada com Data Annotations

### 👤 **Perfis de Usuário**

- 🛡️ **Admin**: Acesso total a todos os endpoints
- ✏️ **Editor**: Acesso apenas para consultar veículos (GET)
- 🔒 **Controle granular**: Cada endpoint tem permissões específicas

### 🚗 **Gerenciamento de Veículos**

- ✅ **CRUD Completo**: Criar, Ler, Atualizar, Deletar
- 📄 **Paginação**: 10 veículos por página
- 🔍 **Filtros**: Busca por nome (case-insensitive)
- ✔️ **Validações**: Ano entre 1886 e ano atual + 1
- 📊 **Dados de teste**: 5 veículos pré-cadastrados

### 🏗️ **Arquitetura**

- **Domain-Driven Design (DDD)**: Separação em camadas
- **JWT Authentication**: Autenticação segura baseada em tokens
- **Role-based Authorization**: Controle de acesso granular (admin/editor)
- **Dependency Injection**: Injeção de dependência nativa do .NET
- **Repository Pattern**: Serviços para acesso aos dados
- **DTO Pattern**: Data Transfer Objects para APIs

---

## �📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- 🔷 [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- 🗄️ [MySQL Server](https://dev.mysql.com/downloads/)
- ⚡ [Ferramenta EF Core CLI](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet)

## ⚙️ Configuração do Projeto

### 1. 📥 **Clone o repositório:**

```bash
git clone https://github.com/JoannaBraccini/minimal-api_DIO.git
cd minimal-api
```

### 2. 🔧 **Configure a string de conexão:**

Edite o arquivo `Api/appsettings.json` e ajuste a string de conexão do banco de dados conforme seu ambiente MySQL.

### 3. 📦 **Restaure os pacotes NuGet:**

```bash
# Restaurar dependências de toda a solution
dotnet restore

# Ou restaurar individualmente
dotnet restore Api/minimal-api.csproj
dotnet restore Test/Test.csproj
```

## 🗃️ Migrações do Banco de Dados

O projeto utiliza o **Entity Framework Core** para gerenciar as migrações do banco de dados de forma eficiente e organizada.

### 🆕 **Criar uma nova migração**

Para criar uma nova migração, navegue até a pasta Api e execute:

```bash
cd Api
dotnet ef migrations add NomeDaMigracao
```

### 🔄 **Atualizar o banco de dados**

Para aplicar as migrações e criar/atualizar o banco de dados, execute:

```bash
cd Api
dotnet ef database update
```

### 🔐 **Configuração JWT (Opcional)**

Por padrão, a aplicação usa uma chave JWT padrão. Para production, configure sua própria chave:

1. **Adicione no `appsettings.json`:**

   ```json
   {
     "Jwt": "sua_chave_secreta_super_segura_com_mais_de_32_caracteres"
   }
   ```

2. **Ou configure via variável de ambiente:**
   ```bash
   export Jwt="sua_chave_secreta"
   ```

> 💡 **Dica**: Tokens JWT expiram em 24 horas. Faça login novamente se receber erro 401.

## � Dados de Teste (Seed Data)

O projeto já vem com dados iniciais para facilitar os testes:

### 👨‍💼 **Administrador Padrão**

- **Email**: `administrador@teste.com`
- **Senha**: `senha123`
- **Perfil**: `admin`

### 🚗 **Veículos Pré-cadastrados**

1. **Honda Civic 2023**
2. **Toyota Corolla 2024**
3. **Volkswagen Golf 2022**
4. **Hyundai HB20 2023**
5. **Chevrolet Onix 2024**

> 💡 **Dica**: Use esses dados para testar os endpoints sem precisar criar registros manualmente!

---

## 🌐 Endpoints da API

A API possui os seguintes endpoints organizados por funcionalidade:

### 🏠 **Home**

- `GET /` - Informações gerais da API _(público)_

### 👨‍💼 **Administradores**

- `POST /administradores/login` - Autenticação _(público)_
- `GET /administradores` - Listar com paginação 🔒 **(admin)**
- `GET /administradores/{id}` - Buscar por ID 🔒 **(admin)**
- `POST /administradores` - Criar novo 🔒 **(admin)**

### 🚗 **Veículos**

- `GET /veiculos` - Listar com paginação e filtros 🔒 **(admin/editor)**
- `GET /veiculos/{id}` - Buscar por ID 🔒 **(admin/editor)**
- `POST /veiculos` - Criar novo 🔒 **(admin)**
- `PUT /veiculos/{id}` - Atualizar existente 🔒 **(admin)**
- `DELETE /veiculos/{id}` - Remover 🔒 **(admin)**

> 🔒 = Endpoint protegido (requer token JWT)  
> **(admin)** = Apenas administradores  
> **(admin/editor)** = Administradores e editores

## �🗄️ Verificando o Banco de Dados

Após aplicar as migrações, você pode verificar se as tabelas foram criadas corretamente:

### 🔌 **Conectar ao MySQL**

```bash
mysql -uroot -p'root'
```

### 📋 **Ver todas as tabelas**

```sql
SHOW TABLES;
```

### 🔍 **Descrever estrutura da tabela Administradores**

```sql
DESC Administradores;
```

### 🚪 **Sair do MySQL**

```sql
exit;
```

## 🏃‍♂️ Executando a API

Para rodar a aplicação localmente:

```bash
# Executar a partir da pasta raiz
dotnet run --project Api

# Ou navegue até a pasta Api
cd Api
dotnet run
```

🌐 A API estará disponível em:

- **HTTPS:** `https://localhost:7020`
- **HTTP:** `http://localhost:5111`

📚 **Documentação Swagger:** `https://localhost:7020/swagger`

### 🔐 **Testando Autenticação JWT no Swagger**

1. **Fazer Login:**

   - Acesse o endpoint `POST /administradores/login`
   - Use as credenciais padrão (perfil **admin**):
     ```json
     {
       "email": "administrador@teste.com",
       "senha": "senha123"
     }
     ```
   - Copie o token JWT retornado

2. **Autorizar no Swagger:**

   - Clique no botão **🔒 Authorize** (canto superior direito)
   - Cole o token no campo **Value**
   - Clique em **Authorize**

3. **Testar Endpoints Protegidos:**
   - Com perfil **admin**: Acesso total a todos os endpoints
   - Com perfil **editor**: Acesso apenas aos GETs de veículos
   - O token será enviado automaticamente nos headers

> ⚠️ **Importante**: Todos os endpoints (exceto `/` e `/administradores/login`) requerem autenticação JWT!
>
> 🔑 **Roles**: Verifique as permissões de cada endpoint na seção de documentação da API.

## 🧪 Executando os Testes

O projeto inclui um projeto de testes unitários usando **MSTest** para garantir a qualidade e confiabilidade do código.

### 🏃‍♂️ **Executar Todos os Testes**

```bash
dotnet test
```

### 🎯 **Executar Testes de um Projeto Específico**

```bash
dotnet test Test/Test.csproj
```

### 📊 **Executar Testes com Relatório de Cobertura**

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### 🔍 **Executar Testes em Modo Verbose**

```bash
dotnet test --logger:"console;verbosity=detailed"
```

### 📋 **Estrutura dos Testes**

O projeto de testes está organizado da seguinte forma:

- 📁 **Test/** - Pasta principal dos testes
  - 🧪 **Test1.cs** - Exemplo de teste unitário
  - 📋 **MSTestSettings.cs** - Configurações dos testes
  - 📦 **Test.csproj** - Dependências e configurações do projeto de teste

### 🛠️ **Tecnologias de Teste Utilizadas**

- **MSTest 3.6.4** - Framework de testes da Microsoft
- **Microsoft.NET.Test.Sdk 17.12.0** - SDK para execução de testes
- **Referência ao projeto Api** - Para testar os serviços e funcionalidades

### 💡 **Dicas para Desenvolvimento de Testes**

1. **Organize por categorias**: Crie classes separadas para testar diferentes serviços
2. **Use nomes descritivos**: Métodos de teste devem explicar claramente o que está sendo testado
3. **Padrão AAA**: Arrange (preparar), Act (executar), Assert (verificar)
4. **Testes isolados**: Cada teste deve ser independente e não depender de outros
5. **Mock de dependências**: Use mocks para isolar as unidades de teste

### 🎯 **Exemplo de Estrutura de Teste**

```csharp
[TestClass]
public class AdministradorServicoTests
{
    [TestMethod]
    public void Login_ComCredenciaisValidas_DeveRetornarAdministradorLogado()
    {
        // Arrange
        var email = "admin@teste.com";
        var senha = "senha123";

        // Act
        var resultado = administradorServico.Login(loginDto);

        // Assert
        Assert.IsNotNull(resultado);
        Assert.AreEqual(email, resultado.Email);
    }
}
```

## 📁 Estrutura do Projeto

```
📦 minimal-api/
├── 🚀 Api/                # Projeto principal da API
│   ├── 🏛️ Dominio/
│   │   ├── 📋 DTOs/           # Data Transfer Objects
│   │   │   ├── AdministradorDTO.cs
│   │   │   ├── LoginDTO.cs
│   │   │   └── VeiculoDTO.cs
│   │   ├── 🏢 Entidades/      # Entidades do negócio
│   │   │   ├── Administrador.cs
│   │   │   └── Veiculo.cs
│   │   ├── 🔧 Enuns/          # Enumeradores
│   │   │   └── Perfil.cs
│   │   ├── 🔗 Interfaces/     # Contratos dos serviços
│   │   │   ├── IAdministradorServico.cs
│   │   │   └── IVeiculoServico.cs
│   │   ├── 📊 ModelViews/     # Modelos de resposta
│   │   │   ├── AdministradorLogado.cs
│   │   │   ├── AdministradorModelView.cs
│   │   │   ├── ErrosDeValidacao.cs
│   │   │   └── Home.cs
│   │   └── ⚙️ Servicos/       # Serviços da aplicação
│   │       ├── AdministradorServico.cs
│   │       └── VeiculoServico.cs
│   ├── 🔧 Infraestrutura/
│   │   └── 🗄️ Db/            # Contexto do banco de dados
│   │       └── DbContexto.cs
│   ├── 📚 Migrations/         # Migrações do Entity Framework
│   ├── 📁 Properties/         # Configurações do projeto
│   │   └── launchSettings.json
│   ├── ⚙️ appsettings.json    # Configurações da aplicação
│   ├── ⚙️ appsettings.Development.json
│   ├── 📦 minimal-api.csproj  # Arquivo do projeto
│   └── 🚀 Program.cs          # Configuração principal da API
├── 🧪 Test/               # Projeto de testes unitários
│   ├── 📋 MSTestSettings.cs   # Configurações dos testes
│   ├── 🧪 Test1.cs           # Exemplo de teste unitário
│   └── 📦 Test.csproj        # Arquivo do projeto de testes
├── 📄 minimal-api.sln     # Solution file
├── 🎯 .vscode/           # Configurações do VS Code
│   └── launch.json       # Configuração de debug
├── 📖 README.md          # Documentação do projeto
├── 📄 LICENSE            # Licença MIT
└── 📋 CONTRIBUTING.md    # Guia de contribuição
```

## ⚠️ Observações Importantes

- ✅ Certifique-se de que o MySQL está rodando e acessível
- 🔧 Ajuste as configurações de ambiente conforme necessário
- 🔐 Mantenha suas credenciais de banco seguras

---

<div align="center">
  
## 🎓 Desenvolvido no Bootcamp da DIO

**Este projeto faz parte do aprendizado em .NET e demonstra:**

- 🏗️ Arquitetura em camadas (Domain, Infrastructure, DTOs)
- 🗄️ Entity Framework Core com MySQL
- 🚀 Minimal APIs do .NET 9
- 📊 Migrations e Seed Data
- 🔐 Sistema de autenticação simples
- ✅ Validações com Data Annotations
- 📄 Paginação e filtros
- 📚 Documentação XML completa
- 🎯 Padrões DDD e Repository
- 🧪 Testes unitários com MSTest
- 📋 Estrutura organizada em projetos separados

## 🛠️ Tecnologias Utilizadas

### **Backend**

- **.NET 9.0** - Framework principal
- **ASP.NET Core Minimal APIs** - Endpoints simplificados
- **Entity Framework Core 9.0.8** - ORM para acesso aos dados
- **Pomelo.EntityFrameworkCore.MySql 9.0.0** - Provider MySQL

### **Banco de Dados**

- **MySQL** - Sistema de gerenciamento de banco de dados
- **Migrations** - Controle de versão do schema
- **Seed Data** - Dados iniciais para testes

### **Testes**

- **MSTest 3.6.4** - Framework de testes unitários da Microsoft
- **Microsoft.NET.Test.Sdk 17.12.0** - SDK para execução de testes
- **Testes Unitários** - Garantia de qualidade do código
- **Cobertura de Testes** - Monitoramento da qualidade

### **Ferramentas de Desenvolvimento**

- **VS Code** - Editor principal
- **Debug Configuration** - Configuração de debug incluída
- **Swagger/OpenAPI** - Documentação automática da API
- **Git** - Controle de versão

### **Padrões e Práticas**

- **Domain-Driven Design (DDD)** - Organização em camadas
- **Repository Pattern** - Encapsulamento de acesso aos dados
- **DTO Pattern** - Transferência segura de dados
- **Dependency Injection** - Injeção de dependências nativa
- **Nullable Reference Types** - Segurança de tipos
- **Test-Driven Development (TDD)** - Desenvolvimento orientado a testes

<br>

---

## 🐛 Debug e Desenvolvimento

### 🔍 **Configuração do Debug no VS Code**

1. **Arquivo `.vscode/launch.json` já configurado** para debug da aplicação
2. **Execute o debug**: Pressione `F5` ou use "Run and Debug"
3. **Coloque breakpoints** nas linhas desejadas para inspecionar variáveis
4. **Inspecione dados**: Variáveis aparecerão na aba "Variables" > "Locals"

### 📊 **Comandos MySQL Úteis**

```sql
-- Conectar ao MySQL
mysql -u root -p

-- Selecionar o banco de dados
USE `minimal-api`;

-- Listar todas as tabelas
SHOW TABLES;

-- Ver estrutura de uma tabela
DESC administradores;
DESC veiculos;

-- Ver dados das tabelas
SELECT * FROM administradores;
SELECT * FROM veiculos;

-- Sair do MySQL
EXIT;
```

### 🌐 **Testando Endpoints**

#### Endpoint de Login de Administrador

- **Método**: `POST`
- **URL**: `https://localhost:7020/administradores/login`
- **Headers**: `Content-Type: application/json`
- **Body**:

```json
{
  "email": "administrador@teste.com",
  "senha": "senha123"
}
```

#### Endpoint de Criação de Administrador

- **Método**: `POST`
- **URL**: `https://localhost:7020/administradores`
- **Headers**: `Content-Type: application/json`
- **Body**:

```json
{
  "email": "novo@admin.com",
  "senha": "senha123",
  "perfil": "admin"
}
```

#### Endpoint de Criação de Veículo

- **Método**: `POST`
- **URL**: `https://localhost:7020/veiculos`
- **Headers**: `Content-Type: application/json`
- **Body**:

```json
{
  "nome": "Civic",
  "marca": "Honda",
  "ano": 2023
}
```

---

## 📄 Licença

Este projeto está licenciado sob a **MIT License** - veja o arquivo [LICENSE](LICENSE) para detalhes.

### 🔓 **O que você pode fazer:**

- ✅ Usar comercialmente
- ✅ Modificar o código
- ✅ Distribuir
- ✅ Uso privado

### 📋 **Condições:**

- 📄 Incluir o copyright e licença
- 📝 Indicar mudanças feitas

---

### 💙 Obrigada DIO pela oportunidade de aprendizado!

<br>

<div align="center">
  
###  Made with 💜 
  
<img src="https://raw.githubusercontent.com/JoannaBraccini/prompts-for-podcast-generate-by-ia/main/src/devpixel.png" alt="Dev Pixel" width="120" />

### DIO BOOTCAMP

</div>

</div>
