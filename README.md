# Proposta de Seguro

## Objetivo

Consiste em 2 serviços cujas as funções são o cadastro/atualização de proposta de seguro no `ProposalService` e contratação da proposta no `ContractService`.

## Arquitetura

Ambos os serviços foram desenvolvidos usando a arquitetura Clean Architecure onde possui 4 projetos: `Api`, `Application`, `Domain` e `Persistence`. 

Ambos os serviços possuem testes unitários utilizando `Xunit`

Exitem 2 projetos que possuem funcionalidades comuns entre os serviços: `Common` e `UnitTest.Common`

### Api

Projeto responsável pela entrada dos dados (Controllers)

### Application

Projeto onde está as regras de negócio

### Domain

Projeto onde está as entidades de domínio e interfaces

### Persistence

Projeto onde está o acesso ao banco de dados e serviços externos

## Serviços

### ProposalService

Responsável por: 
- Criar proposta de seguro 
- Listar propostas 
- Alterar status da proposta (Em Análise, Aprovada, Rejeitada) 
- Expor API REST 

#### Environment Variables
Variable name | Optional | Default value | Exemplo | Description |
--- | :-: | --- | --- | --- |
**ASPNETCORE_ENVIRONMENT** | | | `Development`, `Staging`, `Production` | O ambiente que está sendo feito deploy no momento |
**DATABASE_CONNECTION_STRING** | | | Server=`SERVER`;Port=`PORT`;Database=`DB`;User Id=`USER`;Password=`PWD`; | String de conexão com o banco de dados. [Referência](https://docs.microsoft.com/pt-br/ef/core/miscellaneous/connection-strings) |
**USE_EF_MIGRATION** | X | `false` | `true`, `false` | Flag para iniciar a migração caso o ambiente seja `Development` |
**SEED_DATABASE** | X | `false` | `true`, `false` | Flag para popular as tabelas satelites caso o ambiente seja `Development` |

#### Docker

##### Construindo a imagem da aplicação (docker build)

```bash
docker build --no-cache -t proposal-service:latest -f ProposalService/src/ProposalService.Api/Dockerfile .
```

##### Rodando imagem da aplicação localmente

> Abaixo, um exemplo de como iniciar um container da aplicação na porta `8080` com suas variáveis de ambiente.

```bash
docker run -it --rm -p 8080:80 `
  --name=proposal-service `
  -e DATABASE_CONNECTION_STRING='Server=localhost;Port=5432;Database=proposal;User Id=postgres;Password=Postgres2022!;' `
  -e MIN_LOG_LEVEL='Debug' `
  -e USE_EF_MIGRATION='true' `
  -e SEED_DATABASE='true' `
  -e ASPNETCORE_ENVIRONMENT='Development' `
  proposal-service:latest
```
### ContractService

Responsável por: 
- Contratar uma proposta (somente se Aprovada) 
- Armazenar informações da contratação (ID da proposta, data de contratação) 
- Comunicar-se com o ProposalService para verificar status da proposta 
- Expor API REST 

#### Environment Variables
Variable name | Optional | Default value | Exemplo | Description |
--- | :-: | --- | --- | --- |
**ASPNETCORE_ENVIRONMENT** | | | `Development`, `Staging`, `Production` | O ambiente que está sendo feito deploy no momento |
**DATABASE_CONNECTION_STRING** | | | Server=`SERVER`;Port=`PORT`;Database=`DB`;User Id=`USER`;Password=`PWD`; | String de conexão com o banco de dados. [Referência](https://docs.microsoft.com/pt-br/ef/core/miscellaneous/connection-strings) |
**USE_EF_MIGRATION** | X | `false` | `true`, `false` | Flag para iniciar a migração caso o ambiente seja `Development` |
**PROPOSAL_SERVICE_ADDRESS** | | | `http://localhost:8080/proposalService/api/` | Base url do `ProposalService` |

#### Docker

##### Construindo a imagem da aplicação (docker build)

```bash
docker build --no-cache -t contract-service:latest -f ContractService/src/ContractService.Api/Dockerfile .
```

##### Rodando imagem da aplicação localmente

> Abaixo, um exemplo de como iniciar um container da aplicação na porta `8090` com suas variáveis de ambiente.

```bash
docker run -it --rm -p 8090:80 `
  --name=contract-service `
  -e DATABASE_CONNECTION_STRING='Server=localhost;Port=5432;Database=contract;User Id=postgres;Password=Postgres2022!;' `
  -e MIN_LOG_LEVEL='Debug' `
  -e USE_EF_MIGRATION='true' `
  -e PROPOSAL_SERVICE_ADDRESS='http://localhost:8080/proposalService/api/' `
  -e ASPNETCORE_ENVIRONMENT='Development' `
  contract-service:latest
```

## Executando Serviços

Para executar os serviços basta rodar o seguinte comando

```bash
docker-compose up -d
```

## Requisitos

- .NET 8
- PostgreSQL
- Docker *
- Docker Compose *

_* Caso queira containerizar_


## Tecnlogias utilizadas

- .NET 8
- Entity Framework
- EF Migrations
- FluentValidation
- Xnuit