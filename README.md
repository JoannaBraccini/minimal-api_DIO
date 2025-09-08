# 🚀 Minimal API - Projeto .NET

<div align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 9.0" />
  <img src="https://img.shields.io/badge/Entity_Framework-Core-512BD4?style=for-the-badge&logo=microsoft&logoColor=white" alt="Entity Framework Core" />
  <img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white" alt="MySQL" />
  <img src="https://img.shields.io/badge/DIO-Bootcamp-FF6B35?style=for-the-badge&logo=graduation-cap&logoColor=white" alt="DIO Bootcamp" />
</div>

<br>

> 🎯 **Projeto desenvolvido durante o Bootcamp da DIO (Digital Innovation One)**  
> Uma API minimalista e eficiente construída com as melhores práticas do .NET moderno.

---

## 📋 Pré-requisitos

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

Edite o arquivo `appsettings.json` e ajuste a string de conexão do banco de dados conforme seu ambiente MySQL.

### 3. 📦 **Restaure os pacotes NuGet:**

```bash
dotnet restore
```

## 🗃️ Migrações do Banco de Dados

O projeto utiliza o **Entity Framework Core** para gerenciar as migrações do banco de dados de forma eficiente e organizada.

### 🆕 **Criar uma nova migração**

Para criar uma nova migração chamada `AdministradorMigration`, execute:

```bash
dotnet ef migrations add AdministradorMigration
```

### 🔄 **Atualizar o banco de dados**

Para aplicar as migrações e criar/atualizar o banco de dados, execute:

```bash
dotnet ef database update
```

## 🗄️ Verificando o Banco de Dados

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
dotnet run
```

🌐 A API estará disponível em:

- **HTTPS:** `https://localhost:5001`
- **HTTP:** `http://localhost:5000`

## 📁 Estrutura do Projeto

```
📦 minimal-api/
├── 🏛️ Dominio/
│   ├── 📋 DTOs/           # Data Transfer Objects
│   ├── 🏢 Entidades/      # Entidades do negócio
│   └── ⚙️ Servicos/       # Serviços da aplicação
├── 🔧 Infraestrutura/
│   └── 🗄️ Db/            # Contexto do banco de dados
├── 📚 Migrations/         # Migrações do Entity Framework
└── 🚀 Program.cs          # Configuração principal da API
```

## ⚠️ Observações Importantes

- ✅ Certifique-se de que o MySQL está rodando e acessível
- 🔧 Ajuste as configurações de ambiente conforme necessário
- 🔐 Mantenha suas credenciais de banco seguras

---

<div align="center">
  
## 🎓 Desenvolvido no Bootcamp da DIO

**Este projeto faz parte do aprendizado em .NET e demonstra:**

- 🏗️ Arquitetura em camadas
- 🗄️ Uso do Entity Framework Core
- 🚀 Implementação de APIs mínimas
- 📊 Gerenciamento de migrações

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

#### Endpoint de Login

- **Método**: `POST`
- **URL**: `https://localhost:7020/login`
- **Headers**: `Content-Type: application/json`
- **Body**:

```json
{
  "email": "administrador@teste.com",
  "senha": "senha123"
}
```

### 💙 Obrigada DIO pela oportunidade de aprendizado!

<br>

<div align="center">
  
###  Made with 💜 
  
<img src="https://raw.githubusercontent.com/JoannaBraccini/prompts-for-podcast-generate-by-ia/main/src/devpixel.png" alt="Dev Pixel" width="120" />

### DIO BOOTCAMP

</div>

</div>
