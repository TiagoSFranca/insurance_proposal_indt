# ProposalService

Responsável por: 
- Criar proposta de seguro 
- Listar propostas 
- Alterar status da proposta (Em Análise, Aprovada, Rejeitada) 
- Expor API REST 

## Environment Variables
Variable name | Optional | Default value | Exemplo | Description |
--- | :-: | --- | --- | --- |
**ASPNETCORE_ENVIRONMENT** | | | `Development`, `Staging`, `Production` | O ambiente que está sendo feito deploy no momento |
**DATABASE_CONNECTION_STRING** | | | Server=`SERVER`;Port=`PORT`;Database=`DB`;User Id=`USER`;Password=`PWD`; | String de conexão com o banco de dados. [Referência](https://docs.microsoft.com/pt-br/ef/core/miscellaneous/connection-strings) |
**USE_EF_MIGRATION** | X | `false` | `true`, `false` | Flag para iniciar a migração caso o ambiente seja `Development` |
**SEED_DATABASE** | X | `false` | `true`, `false` | Flag para popular as tabelas satelites caso o ambiente seja `Development` |

## Docker

### Construindo a imagem da aplicação (docker build)

```bash
docker build --no-cache -t proposal-service:latest . 
```

### Rodando imagem da aplicação localmente

> abaixo, um exemplo de como iniciar um container da aplicação na porta `8080` com suas variáveis de ambiente.

```bash
docker run -it --rm -p80:8080 `
  --name=proposal-service `
  -e DATABASE_CONNECTION_STRING='Server=localhost;Port=5432;Database=proposal;User Id=postgres;Password=Postgres2022!;' `
  -e MIN_LOG_LEVEL='Debug' `
  -e USE_EF_MIGRATION='true' `
  -e SEED_DATABASE='true' `
  -e ASPNETCORE_ENVIRONMENT='Development' `
  proposal-service:latest
```
