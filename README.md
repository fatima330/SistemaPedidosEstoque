# SistemaPedidosEstoque

API REST para gerenciamento de clientes, fornecedores, produtos e pedidos, desenvolvida com **.NET 8**, **C#**, **Dapper** e **Oracle Database**.

O projeto foi desenvolvido com uma arquitetura em camadas, buscando separar as responsabilidades entre Controllers, Services, Repositories e acesso ao banco de dados.

---

## Status do projeto

**Backend funcional — concluído**

A API já está conectada ao Oracle e os principais fluxos foram testados através do Swagger.

### Funcionalidades implementadas

* Cadastro de clientes
* Consulta de clientes
* Atualização de clientes
* Exclusão de clientes
* Cadastro de fornecedores
* Consulta de fornecedores
* Atualização de fornecedores
* Exclusão de fornecedores
* Cadastro de produtos
* Consulta de produtos
* Atualização de produtos
* Exclusão de produtos
* Cadastro de pedidos
* Consulta de pedidos
* Atualização de pedidos
* Exclusão de pedidos
* Relacionamento entre fornecedor e produto
* Relacionamento entre cliente e pedido
* Relacionamento entre produto e pedido
* Consultas SQL utilizando `JOIN`

---

# Tecnologias utilizadas

## Backend

* C#
* .NET 8
* ASP.NET Core Web API
* Dapper
* Oracle Managed Data Access
* Swagger / OpenAPI

## Banco de dados

* Oracle Database
* SQL
* Sequences
* Primary Keys
* Foreign Keys
* `NUMBER(1)` para representação de valores booleanos

## Ferramentas

* Visual Studio / VS Code
* Oracle SQL Developer
* Git
* GitHub
* Swagger

---

# Arquitetura

O projeto utiliza uma arquitetura em camadas:

```text
Controller
     ↓
Service
     ↓
Repository
     ↓
Dapper
     ↓
Oracle Database
```

### Controller

Responsável por receber as requisições HTTP e direcioná-las para a camada de serviço.

### Service

Responsável pelas regras de negócio da aplicação.

### Repository

Responsável pelo acesso ao banco de dados utilizando Dapper e SQL.

### Dapper

Micro-ORM utilizado para executar consultas SQL e realizar o mapeamento dos dados para as entidades.

### Oracle

Banco de dados utilizado para persistência das informações.

---

# Estrutura do projeto

```text
SistemaPedidosEstoque
│
├── database
│   ├── 01_create_tables.sql
│   └── 02_create_sequences.sql
│
└── SistemaPedidosEstoque
    │
    ├── Controllers
    │   ├── ClienteController.cs
    │   ├── DatabaseController.cs
    │   ├── FornecedorController.cs
    │   ├── PedidoController.cs
    │   └── ProdutoController.cs
    │
    ├── Data
    │   └── OracleConnectionFactory.cs
    │
    ├── DTOs
    │   ├── Cliente
    │   │   ├── ClienteRequest.cs
    │   │   └── ClienteResponse.cs
    │   │
    │   ├── Fornecedor
    │   │   ├── FornecedorRequest.cs
    │   │   └── FornecedorResponse.cs
    │   │
    │   ├── Pedido
    │   │   ├── PedidoRequest.cs
    │   │   └── PedidoResponse.cs
    │   │
    │   └── Produto
    │       ├── ProdutoRequest.cs
    │       └── ProdutoResponse.cs
    │
    ├── Entities
    │   ├── Cliente.cs
    │   ├── Fornecedor.cs
    │   ├── Pedido.cs
    │   └── Produto.cs
    │
    ├── Interfaces
    │   ├── IClienteRepository.cs
    │   ├── IFornecedorRepository.cs
    │   ├── IPedidoRepository.cs
    │   └── IProdutoRepository.cs
    │
    ├── Repositories
    │   ├── ClienteRepository.cs
    │   ├── FornecedorRepository.cs
    │   ├── PedidoRepository.cs
    │   └── ProdutoRepository.cs
    │
    └── Services
        ├── ClienteService.cs
        ├── FornecedorService.cs
        ├── PedidoService.cs
        └── ProdutoService.cs
```

---

# Banco de dados

O projeto utiliza quatro tabelas principais.

## TB_CLIENTES_SISTEMA

Armazena os clientes.

```text
ID_CLIENTE
CPF
NOME
ENDERECO
EMAIL
DATA_CADASTRO
TELEFONE
```

## TB_FORNECEDOR

Armazena os fornecedores.

```text
ID_FORNECEDOR
NOME
CNPJ
EMAIL
TELEFONE
ATIVO
```

## TB_PRODUTO_SISTEMA

Armazena os produtos.

```text
ID_PRODUTO
ID_FORNECEDOR
NOME
DESCRICAO
TIPO
QTDE_ESTOQUE
ATIVO
DATA_CADASTRO
```

## PEDIDO

Armazena os pedidos realizados.

```text
ID_PEDIDO
ID_CLIENTE
ID_PRODUTO
QUANTIDADE
```

---

# Relacionamentos

O banco possui os seguintes relacionamentos:

```text
TB_FORNECEDOR
      │
      │ ID_FORNECEDOR
      ↓
TB_PRODUTO_SISTEMA
```

Um fornecedor pode estar relacionado a vários produtos.

```text
TB_CLIENTES_SISTEMA
      │
      │ ID_CLIENTE
      ↓
PEDIDO
```

Um cliente pode possuir vários pedidos.

```text
TB_PRODUTO_SISTEMA
      │
      │ ID_PRODUTO
      ↓
PEDIDO
```

Um produto pode aparecer em vários pedidos.

---

# Sequences

O Oracle utiliza sequences para geração dos IDs:

```text
TB_CLIENTES_SISTEMA_SEQ
TB_FORNECEDOR_SEQ
TB_PRODUTO_SISTEMA_SEQ
TB_PEDIDO_SEQ
```

Os repositories utilizam `NEXTVAL` para gerar automaticamente os identificadores.

Exemplo:

```sql
TB_CLIENTES_SISTEMA_SEQ.NEXTVAL
```

---

# DTOs

O projeto utiliza DTOs para separar os dados recebidos e enviados pela API das entidades utilizadas internamente.

```text
DTOs
├── Cliente
├── Fornecedor
├── Produto
└── Pedido
```

Cada recurso possui:

```text
Request
Response
```

---

# Entidades

As principais entidades do sistema são:

```text
Cliente
Fornecedor
Produto
Pedido
```

### Cliente

Possui informações como:

* CPF
* Nome
* Endereço
* Email
* Telefone
* Data de cadastro

### Fornecedor

Possui:

* Nome
* CNPJ
* Email
* Telefone
* Ativo

### Produto

Possui:

* Fornecedor
* Nome
* Descrição
* Tipo
* Quantidade em estoque
* Ativo
* Data de cadastro

### Pedido

Possui:

* Cliente
* Produto
* Quantidade

---

# Dapper

O acesso ao banco é realizado utilizando Dapper.

Exemplo de consulta:

```csharp
var sql = """
    SELECT
        ID_PRODUTO AS Id,
        ID_FORNECEDOR AS FornecedorId,
        NOME AS Nome,
        DESCRICAO AS Descricao,
        TIPO AS Tipo,
        QTDE_ESTOQUE AS QtdeEstoque,
        ATIVO AS Ativo,
        DATA_CADASTRO AS DataCadastro
    FROM TB_PRODUTO_SISTEMA
    """;
```

O Dapper realiza o mapeamento do resultado SQL para as entidades da aplicação.

---

# OracleConnectionFactory

A conexão com o Oracle foi centralizada através da classe:

```text
Data/OracleConnectionFactory.cs
```

A connection string é armazenada no `appsettings.json`.

Por segurança, credenciais reais não devem ser enviadas para o GitHub.

Exemplo de estrutura:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost:1522/FREEPDB1;User Id=SISTEMA_PEDIDOS;Password=SUA_SENHA;"
  }
}
```

---

# Swagger

A API possui documentação através do Swagger.

Com o projeto em execução, o Swagger permite testar os endpoints diretamente pelo navegador.

Os principais recursos são:

```text
Cliente
Fornecedor
Produto
Pedido
```

O Swagger foi utilizado durante o desenvolvimento para testar os métodos `GET`, `POST`, `PUT` e `DELETE`.

---

# Exemplos de relacionamentos

Um produto pertence a um fornecedor através de:

```text
Produto.FornecedorId
        ↓
TB_FORNECEDOR.ID_FORNECEDOR
```

Um pedido relaciona um cliente e um produto:

```text
Pedido.IdCliente
        ↓
TB_CLIENTES_SISTEMA.ID_CLIENTE

Pedido.IdProduto
        ↓
TB_PRODUTO_SISTEMA.ID_PRODUTO
```

---

# Consulta utilizando JOIN

Também foram realizados testes de consultas relacionando as tabelas.

Exemplo:

```sql
SELECT
    P.ID_PEDIDO,
    C.NOME AS CLIENTE,
    PR.NOME AS PRODUTO,
    F.NOME AS FORNECEDOR,
    P.QUANTIDADE,
    PR.QTDE_ESTOQUE,
    CASE
        WHEN PR.ATIVO = 1 THEN 'Ativo'
        ELSE 'Inativo'
    END AS STATUS_PRODUTO
FROM PEDIDO P
INNER JOIN TB_CLIENTES_SISTEMA C
    ON P.ID_CLIENTE = C.ID_CLIENTE
INNER JOIN TB_PRODUTO_SISTEMA PR
    ON P.ID_PRODUTO = PR.ID_PRODUTO
LEFT JOIN TB_FORNECEDOR F
    ON PR.ID_FORNECEDOR = F.ID_FORNECEDOR
ORDER BY P.ID_PEDIDO;
```

Essa consulta permite visualizar o pedido juntamente com cliente, produto e fornecedor.

---

# Como executar o projeto

## 1. Clonar o repositório

```bash
git clone URL_DO_REPOSITORIO
```

## 2. Acessar o projeto

```bash
cd SistemaPedidosEstoque
```

## 3. Configurar o Oracle

É necessário possuir um banco Oracle disponível e configurar a connection string no:

```text
appsettings.json
```

Não utilize credenciais reais diretamente no repositório público.

## 4. Executar os scripts SQL

Os scripts estão na pasta:

```text
database
```

Execute:

```text
01_create_tables.sql
02_create_sequences.sql
```

## 5. Restaurar os pacotes

```bash
dotnet restore
```

## 6. Executar a API

```bash
dotnet run
```

## 7. Abrir o Swagger

Com a aplicação em execução, acessar a URL apresentada pelo projeto para abrir a documentação Swagger.

---

# Dados de teste

Durante o desenvolvimento foram cadastrados dados de teste para validar os relacionamentos entre:

```text
Clientes
Fornecedores
Produtos
Pedidos
```

A base foi utilizada para testar os métodos CRUD e consultas com `JOIN`.

---

# Próxima etapa — Front-end

Após a conclusão do backend, o próximo objetivo do projeto será desenvolver uma interface web para consumir a API.

## Tecnologias planejadas

* TypeScript
* Angular
* HTML
* CSS
* Consumo de API REST
* HTTP Client do Angular

A arquitetura planejada será:

```text
                  ┌──────────────────────┐
                  │      Angular         │
                  │    TypeScript        │
                  └──────────┬───────────┘
                             │
                             │ HTTP
                             ↓
                  ┌──────────────────────┐
                  │   ASP.NET Core API   │
                  │       .NET 8         │
                  └──────────┬───────────┘
                             │
                             ↓
                  ┌──────────────────────┐
                  │       Dapper         │
                  └──────────┬───────────┘
                             │
                             ↓
                  ┌──────────────────────┐
                  │       Oracle         │
                  └──────────────────────┘
```

---

# Próximas implementações

### Front-end Angular

* Criar projeto Angular
* Configurar TypeScript
* Criar componentes
* Criar serviços para consumo da API
* Configurar rotas
* Criar telas de clientes
* Criar telas de fornecedores
* Criar telas de produtos
* Criar telas de pedidos
* Criar formulários
* Implementar listagem, cadastro, edição e exclusão
* Integrar Angular com a API .NET
* Trabalhar com tratamento de erros
* Melhorar experiência e organização da interface

### Evolução futura

Depois da integração entre Angular e API, poderão ser implementados:

* Autenticação
* Autorização
* Dashboard
* Controle de estoque mais completo
* Filtros e pesquisas
* Paginação
* Validações
* Melhorias de segurança
* Deploy da aplicação

---

# Objetivo do projeto

O objetivo do `SistemaPedidosEstoque` é construir uma aplicação completa, passando pelas principais etapas de desenvolvimento de software:

```text
Banco de dados
      ↓
API REST
      ↓
Regras de negócio
      ↓
Persistência
      ↓
Documentação Swagger
      ↓
Front-end Angular
      ↓
Aplicação completa
```

O projeto também tem como objetivo colocar em prática conceitos de **C#, .NET, APIs REST, arquitetura em camadas, SQL, Oracle, Dapper, TypeScript e Angular**.

---

# Status atual

```text
[x] Banco Oracle
[x] Tabelas
[x] Sequences
[x] Relacionamentos
[x] Entidades
[x] DTOs
[x] Interfaces
[x] Repositories
[x] Services
[x] Controllers
[x] Dapper
[x] Conexão com Oracle
[x] CRUD de Clientes
[x] CRUD de Fornecedores
[x] CRUD de Produtos
[x] CRUD de Pedidos
[x] Swagger
[x] Testes da API
[x] Consultas com JOIN
[ ] Front-end Angular
[ ] Integração Angular + API
[ ] Autenticação
[ ] Deploy
```

---

## Autor

Projeto desenvolvido como prática de desenvolvimento **backend com C#/.NET**, banco de dados Oracle e, futuramente, **frontend com TypeScript e Angular**.
