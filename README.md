# � Sistema de Gerenciamento de Veículos

<div align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 9.0" />
  <img src="https://img.shields.io/badge/Entity_Framework-Core-512BD4?style=for-the-badge&logo=microsoft&logoColor=white" alt="Entity Framework Core" />
  <img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white" alt="MySQL" />
  <img src="https://img.shields.io/badge/JWT-Bearer-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" alt="JWT Bearer" />
  <img src="https://img.shields.io/badge/Testes-98_✅-00C851?style=for-the-badge&logo=checkmarx&logoColor=white" alt="98 Testes" />
  <img src="https://img.shields.io/badge/DIO-Bootcamp-FF6B35?style=for-the-badge&logo=graduation-cap&logoColor=white" alt="DIO Bootcamp" />
</div>

<br>

<div align="center">
  
**🎯 Uma API moderna e completa para gerenciar veículos**

_Desenvolvida durante o Bootcamp da GFT em parceria com a DIO com foco em qualidade e boas práticas_

[🚀 Como Usar](#-como-usar) • [📱 Testar API](#-testando-a-api) • [🔧 Documentação Técnica](TECHNICAL_SUMMARY.md)

</div>

---

## 🌟 **Por que este projeto é especial?**

Este não é apenas mais uma API, é um **projeto de portfólio completo** que demonstra:

- ✅ **98 testes automatizados** - Qualidade garantida!
- � **Sistema de login seguro** com JWT e diferentes níveis de acesso
- 🏗️ **Código limpo e bem estruturado** seguindo padrões da indústria
- 📚 **Documentação completa** - Fácil de entender e manter
- 🚀 **Performance otimizada** com Minimal APIs do .NET 9

---

## � Funcionalidades

### 🔐 **Autenticação de Administradores**

- Login com email e senha retornando token JWT
- Sistema de perfis (admin/editor) usando enums
- Autenticação Bearer Token para endpoints protegidos
- Tokens com expiração de 24 horas
- Validações de entrada com Data Annotations

### 👤 **Perfis de Usuário**

- 🛡️ **Admin**: Acesso total a todos os endpoints (CRUD completo)
- ✏️ **Editor**: Acesso limitado apenas para consultar veículos (somente GET)
- 🔒 **Controle granular**: Cada endpoint tem permissões específicas

### 🎮 **O que cada perfil pode fazer?**

#### 👨‍💼 **Como Administrador (admin)**

- 🔑 **Fazer login** e receber token de acesso completo
- 👥 **Gerenciar administradores** (criar, consultar)
- 🚗 **Controle total de veículos** (criar, editar, excluir, consultar)
- 📊 **Acesso a todos os endpoints** da API

#### ✏️ **Como Editor (editor)**

- 🔑 **Fazer login** com permissões limitadas
- 👀 **Consultar veículos** (apenas visualização)
- 🚫 **Sem permissão** para criar, editar ou excluir
- 📋 **Focado em consultas** e relatórios

#### 🌐 **Usuário Anônimo**

- 📋 **Ver informações da API** na página inicial
- 📚 **Acessar documentação** via Swagger UI
- 🚫 **Sem acesso** aos dados protegidos

## 📱 **Testando a API**

### **🔑 Credenciais de Teste**

**👨‍💼 Admin (acesso total):**

- Email: `administrador@teste.com`
- Senha: `senha123`

**✏️ Editor (apenas consultas - disponível nos testes):**

- Email: `editor@teste.com`
- Senha: `senha123`

> ⚠️ **Nota**: O usuário editor está disponível apenas nos **testes automatizados**. Na aplicação real, apenas o admin é criado por padrão. Para criar um editor na aplicação:
>
> 1. Faça login como admin
> 2. Use o endpoint `POST /administradores` para criar um novo usuário com `"perfil": "editor"`
> 3. O novo editor poderá fazer login e acessar apenas os endpoints de consulta de veículos

### **🚀 Teste Rápido - Admin**

```bash
# 1. Login como Admin
curl -X POST "https://localhost:7020/administradores/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "administrador@teste.com",
    "senha": "senha123"
  }'

# 2. Use o token para listar veículos
curl -X GET "https://localhost:7020/veiculos" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"

# 3. Criar um novo veículo (apenas admin pode)
curl -X POST "https://localhost:7020/veiculos" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Fusca",
    "marca": "Volkswagen",
    "ano": 1975
  }'
```

### **🌐 Explore no Navegador**

- **Página inicial**: https://localhost:7020
- **Documentação Swagger**: https://localhost:7020/swagger

> 💡 **Dica**: No Swagger, use o botão 🔒 **Authorize** para inserir o token JWT e testar todos os endpoints!

---

## 🎯 **Para Desenvolvedores**

### **🧪 Executar Testes**

```bash
# Todos os testes (98 testes)
dotnet test

# Apenas testes unitários
dotnet test --filter "Category!=Integration"

# Com detalhes verbose
dotnet test --verbosity detailed
```

### **🔧 Desenvolvimento**

```bash
# Executar em modo de desenvolvimento
dotnet run --project Api

# Watch mode (reinicia automaticamente)
dotnet watch run --project Api
```

````

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
````

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

O projeto inclui uma **suíte completa de testes** com **98 testes** cobrindo todas as funcionalidades principais da aplicação usando **MSTest** e **WebApplicationFactory**.

### 📊 **Estatísticas dos Testes**

- ✅ **98 testes** em execução
- 🎯 **100% de sucesso** nos testes
- 🧪 **Cobertura completa**: Unitários + Integração + Request
- ⚡ **Execução rápida**: ~4 segundos para toda a suíte

### 🏃‍♂️ **Executar Todos os Testes**

```bash
dotnet test
```

### 🎯 **Executar Testes Específicos**

```bash
# Testes de domínio (unitários)
dotnet test --filter "FullyQualifiedName~Domain"

# Testes de request (integração)
dotnet test --filter "FullyQualifiedName~RequestTest"

# Testes de helpers e utilitários
dotnet test --filter "FullyQualifiedName~Helper"
```

### 📊 **Executar Testes com Relatório de Cobertura**

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### 🔍 **Executar Testes em Modo Verbose**

```bash
dotnet test --logger:"console;verbosity=detailed"
```

### 📋 **Estrutura Completa dos Testes**

```
🧪 Test/
├── 🏠 Requests/                    # Testes de integração HTTP
│   ├── AdministradorRequestTest.cs     # Endpoints de administradores
│   ├── AdministradorRequestTestComAuth.cs # Testes com autenticação JWT
│   ├── VeiculoRequestTest.cs           # CRUD completo de veículos
│   └── HomeRequestTest.cs              # Endpoint público
├── 🎯 Domain/                      # Testes unitários do domínio
│   ├── DTOs/                          # Testes dos Data Transfer Objects
│   │   ├── AdministradorDTOTest.cs
│   │   ├── LoginDTOTest.cs
│   │   └── VeiculoDTOTest.cs
│   ├── Entidades/                     # Testes das entidades de negócio
│   │   ├── AdministradorTest.cs
│   │   └── VeiculoTest.cs
│   ├── Enuns/                         # Testes dos enumeradores
│   │   └── PerfilTest.cs
│   ├── ModelViews/                    # Testes dos modelos de resposta
│   │   ├── AdministradorLogadoTest.cs
│   │   ├── AdministradorModelViewTest.cs
│   │   ├── ErrosDeValidacaoTest.cs
│   │   └── HomeTest.cs
│   └── Servicos/                      # Testes dos serviços de negócio
│       ├── AdministradorServicoTest.cs
│       └── VeiculoServicoTest.cs
├── 🔧 Helpers/                     # Helpers para testes
│   ├── Setup.cs                       # Configuração do WebApplicationFactory
│   ├── JwtValidacaoHelper.cs          # Helper para autenticação JWT
│   └── ValidacaoHelperTest.cs         # Testes das validações
└── 🎭 Mocks/                       # Mocks para isolamento de testes
    ├── AdministradorServicoMock.cs    # Mock do serviço de administradores
    └── VeiculoServicoMock.cs          # Mock do serviço de veículos
```

### 🛠️ **Tecnologias de Teste Utilizadas**

- **MSTest 3.6.4** - Framework de testes da Microsoft
- **Microsoft.AspNetCore.Mvc.Testing 9.0.0** - Testes de integração HTTP
- **Microsoft.NET.Test.Sdk 17.12.0** - SDK para execução de testes
- **WebApplicationFactory** - Factory para criação de servidor de teste
- **JWT Helper** - Utilitários para autenticação em testes

### � **Testes de Autenticação JWT**

Os testes incluem **autenticação completa JWT** usando:

- **Login automático** com credenciais de teste
- **Tokens temporários** para testes isolados
- **Validação de permissões** por perfil (admin/editor)
- **Cleanup automático** de tokens após testes

### 💡 **Padrões Utilizados nos Testes**

1. **Arrange-Act-Assert (AAA)**: Estrutura clara e consistente
2. **Isolation**: Cada teste é independente usando mocks
3. **Integration Testing**: Testes end-to-end com WebApplicationFactory
4. **JWT Authentication**: Testes reais de autenticação e autorização
5. **Cleanup**: Limpeza automática após cada teste

### 🎯 **Exemplo de Teste de Integração com JWT**

```csharp
[TestMethod]
public async Task TestarCriarVeiculo()
{
    // Arrange - usando helper JWT para autenticação
    var response = await JwtValidacaoHelper.ComTokenTemporario(
        Setup.client,
        Perfil.admin,
        async () => await Setup.client.PostAsync("/veiculos", content)
    );

    // Assert
    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
}
```

## 📁 Estrutura do Projeto

```
📦 minimal-api/
├── 🚀 Api/                           # Projeto principal da API
│   ├── 🏛️ Dominio/
│   │   ├── 📋 DTOs/                     # Data Transfer Objects
│   │   │   ├── AdministradorDTO.cs
│   │   │   ├── LoginDTO.cs
│   │   │   └── VeiculoDTO.cs
│   │   ├── 🏢 Entidades/                # Entidades do negócio
│   │   │   ├── Administrador.cs
│   │   │   └── Veiculo.cs
│   │   ├── 🔧 Enuns/                    # Enumeradores
│   │   │   └── Perfil.cs
│   │   ├── 🔗 Interfaces/               # Contratos dos serviços
│   │   │   ├── IAdministradorServico.cs
│   │   │   └── IVeiculoServico.cs
│   │   ├── 📊 ModelViews/               # Modelos de resposta
│   │   │   ├── AdministradorLogado.cs
│   │   │   ├── AdministradorModelView.cs
│   │   │   ├── ErrosDeValidacao.cs
│   │   │   └── Home.cs
│   │   └── ⚙️ Servicos/                 # Serviços da aplicação
│   │       ├── AdministradorServico.cs
│   │       └── VeiculoServico.cs
│   ├── 🔧 Infraestrutura/
│   │   └── 🗄️ Db/                      # Contexto do banco de dados
│   │       └── DbContexto.cs
│   ├── 📚 Migrations/                   # Migrações do Entity Framework
│   ├── 📁 Properties/                   # Configurações do projeto
│   │   └── launchSettings.json
│   ├── ⚙️ appsettings.json              # Configurações da aplicação
│   ├── ⚙️ appsettings.Development.json
│   ├── � Startup.cs                    # Configuração da aplicação
│   ├── �📦 minimal-api.csproj            # Arquivo do projeto
│   └── 🚀 Program.cs                    # Entry point da aplicação
├── 🧪 Test/                             # Projeto completo de testes (98 testes)
│   ├── 🏠 Requests/                     # Testes de integração HTTP
│   │   ├── AdministradorRequestTest.cs      # Endpoints de administradores
│   │   ├── AdministradorRequestTestComAuth.cs # Testes com autenticação JWT
│   │   ├── VeiculoRequestTest.cs            # CRUD completo de veículos
│   │   └── HomeRequestTest.cs               # Endpoint público
│   ├── 🎯 Domain/                       # Testes unitários do domínio
│   │   ├── 📋 DTOs/                     # Testes dos Data Transfer Objects
│   │   │   ├── AdministradorDTOTest.cs
│   │   │   ├── LoginDTOTest.cs
│   │   │   └── VeiculoDTOTest.cs
│   │   ├── 🏢 Entidades/                # Testes das entidades de negócio
│   │   │   ├── AdministradorTest.cs
│   │   │   └── VeiculoTest.cs
│   │   ├── 🔧 Enuns/                    # Testes dos enumeradores
│   │   │   └── PerfilTest.cs
│   │   ├── 📊 ModelViews/               # Testes dos modelos de resposta
│   │   │   ├── AdministradorLogadoTest.cs
│   │   │   ├── AdministradorModelViewTest.cs
│   │   │   ├── ErrosDeValidacaoTest.cs
│   │   │   └── HomeTest.cs
│   │   └── ⚙️ Servicos/                 # Testes dos serviços de negócio
│   │       ├── AdministradorServicoTest.cs
│   │       └── VeiculoServicoTest.cs
│   ├── 🔧 Helpers/                      # Helpers para testes
│   │   ├── Setup.cs                         # Configuração do WebApplicationFactory
│   │   ├── JwtValidacaoHelper.cs            # Helper para autenticação JWT
│   │   └── ValidacaoHelperTest.cs           # Testes das validações
│   ├── 🎭 Mocks/                        # Mocks para isolamento de testes
│   │   ├── AdministradorServicoMock.cs      # Mock do serviço de administradores
│   │   └── VeiculoServicoMock.cs            # Mock do serviço de veículos
│   ├── 🎲 Scenarios/                    # Cenários de teste específicos
│   │   ├── CenarioAutenticacaoTest.cs       # Cenários de autenticação
│   │   └── CenarioVeiculosTest.cs           # Cenários completos de veículos
│   ├── 📊 Integracao/                   # Testes de integração específicos
│   ├── 📋 MSTestSettings.cs             # Configurações dos testes MSTest
│   ├── 📋 GlobalUsings.cs               # Using statements globais
│   └── 📦 Test.csproj                   # Arquivo do projeto de testes
├── 📄 minimal-api.sln                   # Solution file
├── 🎯 .vscode/                          # Configurações do VS Code
│   └── launch.json                          # Configuração de debug
├── 📖 README.md                         # Documentação do projeto
├── 📄 LICENSE                           # Licença MIT
└── 📋 CONTRIBUTING.md                   # Guia de contribuição
```

## ⚠️ Observações Importantes

- ✅ Certifique-se de que o MySQL está rodando e acessível
- 🔧 Ajuste as configurações de ambiente conforme necessário
- 🔐 Mantenha suas credenciais de banco seguras

---

<div align="center">
  
## 🎓 Desenvolvido no Bootcamp da GFT em parceria com a DIO

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
- **Microsoft.AspNetCore.Mvc.Testing 9.0.0** - Testes de integração HTTP
- **Microsoft.NET.Test.Sdk 17.12.0** - SDK para execução de testes
- **WebApplicationFactory** - Factory para testes de integração
- **JWT Helper** - Utilitários customizados para autenticação em testes
- **Mock Services** - Isolamento de dependências para testes unitários
- **98 Testes** - Cobertura completa: unitários + integração + request

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

## 🏗️ **Estrutura do Projeto**

```
� minimal-api/
├── 🚀 Api/                     # API Principal
│   ├── 📋 Program.cs                # Configuração e endpoints
│   ├── ⚙️ Startup.cs               # Configuração de serviços
│   └── 📂 Dominio/                 # Lógica de negócio
│       ├── 📦 DTOs/                   # Transferência de dados
│       ├── 🏢 Entidades/              # Modelos de negócio
│       ├── 📋 Enuns/                  # Enumeradores
│       ├── 🔌 Interfaces/             # Contratos
│       ├── 📊 ModelViews/             # Respostas da API
│       └── ⚙️ Servicos/               # Lógica de negócio
└── 🧪 Test/                    # Testes Automatizados
    ├── 📱 Requests/                # Testes de endpoints
    ├── 🎯 Domain/                  # Testes unitários
    ├── 🔧 Helpers/                 # Utilitários de teste
    └── 🎭 Mocks/                   # Mocks para isolamento
```

---

## 🤝 **Como Contribuir**

Quer melhorar este projeto? Toda contribuição é bem-vinda!

1. **🍴 Fork** o projeto
2. **🌿 Crie** uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. **✅ Commit** suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. **📤 Push** para a branch (`git push origin feature/MinhaFeature`)
5. **🔀 Abra** um Pull Request

### 📋 **Checklist antes de contribuir:**

- [ ] Testes passando (`dotnet test`)
- [ ] Código documentado
- [ ] Seguindo padrões do projeto

---

## 🎓 **Aprendizados e Tecnologias**

Este projeto demonstra conhecimentos em:

### **Backend**

- ✅ **.NET 9** - Framework moderno
- ✅ **Minimal APIs** - Performance otimizada
- ✅ **Entity Framework Core** - ORM robusto
- ✅ **JWT Authentication** - Segurança
- ✅ **MySQL** - Banco de dados

### **Qualidade**

- ✅ **98 Testes Automatizados** - Confiabilidade
- ✅ **Testes de Integração** - Cobertura completa
- ✅ **Documentação XML** - Código documentado
- ✅ **Clean Code** - Boas práticas

### **DevOps**

- ✅ **Git Flow** - Controle de versão
- ✅ **CI/CD Ready** - Pronto para deploy
- ✅ **Docker Ready** - Containerização
- ✅ **Environment Config** - Configuração flexível

---

## 📚 **Documentação Adicional**

- 📋 **[Resumo Técnico Completo](TECHNICAL_SUMMARY.md)** - Para desenvolvedores
- 📖 **[Como Contribuir](CONTRIBUTING.md)** - Guia de contribuição
- 🔐 **[Swagger UI](https://localhost:7020/swagger)** - Documentação interativa da API

---

## 📞 **Contato e Suporte**

- 🐛 **Encontrou um bug?** Abra uma [issue](../../issues)
- 💡 **Tem uma sugestão?** Crie uma [discussion](../../discussions)
- 💬 **Quer conversar?** Me encontre no [LinkedIn](https://linkedin.com/in/joannabraccini)

---

<div align="center">

## 🌟 **Se este projeto te ajudou, deixe uma ⭐!**

**Feito com 💜 durante o Bootcamp GFT Start #7**

_Transformando conhecimento em código de qualidade_

---

### 🎯 **Status do Projeto: ✅ COMPLETO**

_98 testes • 100% funcional • Pronto para produção_

</div>
