# 📦 Sistema de Gestão de Estoque de Produtos (MVP)

Sistema web para gerenciamento de usuários, produtos, setores, estoque e movimentações internas, desenvolvido em **ASP.NET MVC 5 / Web API 2** com **.NET Framework 4.8**, seguindo princípios de organização em camadas e boas práticas de desenvolvimento.

---

# 📋 Visão Geral

O sistema permite o cadastro de produtos, controle de estoque por setor e registro de movimentações de entrada, consumo e transferência entre setores.

Todas as movimentações ficam registradas para rastreabilidade.

## Principais Funcionalidades do Sistema

| Módulo                                  | Funcionalidades                                                                                                                            |
| --------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------ |
| **Controle de Estoque**                 | CRUD completo de produtos                                                                                                                  |
| **Geração de Códigos Unitários (SKU):** | Geração e associação de um código SKU único a cada produto cadastrado.                                                                     |
| **Login:**                              | Implementação de login utilizando o CPF como identificador do usuário.                                                                     |
| **Entrada de Produtos:**                | Funcionalidade para registrar a aquisição de novos produtos.                                                                               |
| **Consumo de Produtos**                 | Funcionalidade para registrar a baixa de produtos do estoque por consumo interno.                                                          |
| **Envio de Produtos entre Setores:**    | Funcionalidade para realizar a transferência de itens entre diferentes setores, debitando o estoque de um e creditando o estoque do outro. |

---

---

## 📌 Regras de Negócio

- **Controle de estoque:** Implementar lógica básica de controle de estoque (por exemplo, impedir saída de produto se a quantidade em estoque for zero).
- **Transferência::** Toda Entrada, Consumo ou Envio entre Setores deve gerar um registro na tabela de movimentação para rastreabilidade..
- **Quantidade de produtos:** A quantidade de produtos em um Envio entre Setores ou Consumo não pode ser superior à quantidade em estoque.

---

# 🏗️ Arquitetura

O projeto está organizado em camadas:

```text
┌──────────────────────────────────────────────┐
│              WebApplication                  │ ← MVC + Web API
├──────────────────────────────────────────────┤
│               Application                    │ ← DTOs, Services
├──────────────────────────────────────────────┤
│                 Domain                       │ ← Entidades, Interfaces
├──────────────────────────────────────────────┤
│               Persistence                    │ ← DbContext, Repositórios
└──────────────────────────────────────────────┘
```
---

## 🛠️ Tecnologias e Dependências

| Tecnologia | Versão | Finalidade |
|---|---|---|
| .NET Framework | 4.8 | Runtime da aplicação |
| ASP.NET MVC | 5.2.9 | Interface web (Views Razor) |
| ASP.NET Web API | 5.2.9 | API REST |
| Entity Framework | 6.5.1 | ORM — acesso a dados |
| SQL Server | 2022 | Banco de dados relacional |
| AutoMapper | 9.0.0 | Mapeamento Entidade ↔ DTO |
| Simple Injector | 5.5.0 | Container de injeção de dependência |
| Swashbuckle | 5.6.0 | Documentação Swagger / Swagger UI |
| Newtonsoft.Json | 13.0.1 | Serialização JSON |
| Bootstrap | 5.x | Framework CSS para as Views |

---


## ✅ Pré-requisitos

Antes de executar o projeto, certifique-se de ter instalado:

- [**Visual Studio 2019+**](https://visualstudio.microsoft.com/) com a carga de trabalho **ASP.NET e desenvolvimento Web**
- [**.NET Framework 4.8 Developer Pack**](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [**SQL Server 2019+**](https://www.microsoft.com/sql-server) (local)
- [**Git**](https://git-scm.com/)

---

## 🗄️ Configuração do Banco de Dados

O projeto utiliza **SQL Server** com o banco `LigaEstoqueDB`

1. Conecte-se à sua instância do SQL Server (SSMS, Azure Data Studio, etc.).
2. Execute o script `Script BD.sql` localizado na raiz do repositório. Ele irá:
   - Criar o banco `LigaEstoqueDB`
   - Criar as tabelas `Produto`, `Usuários,` `Setores` e `MovimentacaoEstoque`
   - Inserir dados
3. Ajuste a connection string no arquivo `WebApplication\Web.config` se necessário:

```xml
<connectionStrings>
  <add name="LigaDbConnection" connectionString="Server=localhost,1433;Database=LigaEstoqueDB;Trusted_Connection=True;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

> **Importante:** Caso use uma instância nomeada (ex.: `.\SQLEXPRESS`), autenticação Windows ou porta diferente, altere os valores de `Server`, `User Id` e `Password` de acordo com seu ambiente.

## Executando a Aplicação

### 1. Clonar o repositório

```bash
git clone https://github.com/danielequerino7/Sistema-Gestao-Estoque.git
```

### 2. Criar e subir o banco de dados

execute o `Script DB.sql` manualmente conforme explicado na seção anterior.

### 3. Restaurar pacotes NuGet

Abra a solução `.sln` no **Visual Studio**. Os pacotes serão restaurados automaticamente no build. Caso contrário, execute:

```
Menu → Ferramentas → Gerenciador de Pacotes NuGet → Console do Gerenciador de Pacotes
PM> Update-Package -reinstall
```

### 4. Compilar e executar

1. Defina o projeto **WebApplication** como projeto de inicialização (clique com botão direito → _Definir como Projeto de Inicialização_).
2. Pressione **F5** (ou **Ctrl+F5** para executar sem depuração).
3. O IIS Express iniciará a aplicação.

### 5. Acessar o sistema

| Recurso            | URL                          |
| ------------------ | ---------------------------- |
| **Página Inicial** | `https://localhost:{porta}/` |

> A porta é atribuída automaticamente pelo IIS Express. Verifique a URL na barra de endereços do navegador ou nas propriedades do projeto.

## 📡 Endpoints da API

**Swagger (Documentação da API)** | `https://localhost:{porta}/swagger`

## 📂 Estrutura de Pastas

```
├── Application/
│   ├── DTO/                  # Data Transfer Objects
│   ├── Exceptions/           # Exceções de serviço
│   ├── Mappings/             # Perfis AutoMapper
│   └── Services/             # Serviços de aplicação + interfaces
├── Domain/
│   ├── Entities/             # Entidades de domínio
│   └── Interfaces/           # Contratos de repositório
├── Persistence/
│   ├── Context/              # DbContext (Entity Framework)
│   └── Repositories/         # Implementação dos repositórios
├── WebApplication/
│   ├── API/                  # Controllers Web API
│   ├── App_Start/            # Configurações (rotas, Swagger, Web API)
│   ├── Controllers/          # Controllers MVC
│   ├── Exceptions/           # Filtro de exceções da API
│   ├── ViewsModel/           # Modelos
│   ├── Views/                # Views Razor + Layout
│   └── Web.config            # Configuração e connection string
├── Scripts SQL/
```
