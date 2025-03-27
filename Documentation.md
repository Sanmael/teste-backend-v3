# Sistema de Faturamento de Teatro (Theatrical Players Billing System)

## Visão Geral
Sistema desenvolvido para gerenciar o faturamento de apresentações teatrais, implementando Clean Architecture, DDD, SOLID e padrões de projeto. 
O sistema processa pedidos de faturamento de forma assíncrona usando filas.

## Arquitetura

### Domain Layer
- **Entidades**:
  - `Invoice`: Representa uma fatura
  - `Performance`: Representa uma apresentação teatral
  - `Play`: Representa uma peça teatral
- **Value Objects**:
  - `CustomerName`: Nome do cliente
  - `Audience`: Público presente
  - `Lines`: Número de linhas da peça
  - `PlayType`: Tipo da peça (Tragedy/Comedy/History)
- **Factories**:
  - `BillingStrategyFactory` : Define qual Strategy vai ser instanciado dependendo do Type da peça (Tragedy/Comedy/History)
- **Strategies**:
  - `IBillingStrategy` : Interface que define os metodos de calcular o Amount e os Creditos
  - `ComedyBillingStrategy` : Calcula o Amount e os Creditos da peça de Comedy
  - `HistoryBillingStrategy`: Calcula o Amount e os Creditos da peça de History
  - `TragedyBillingStrategy`: Calcula o Amount e os Creditos da peça de Tragedy

### Application Layer
- Implementa casos de uso usando CQRS pattern
- Commands:
  - `GenerateBillCommand`: Gera nova fatura
- Queries:
  - `GetBillQuery`: Recupera fatura gerada
- Validação usando FluentValidation
- Mediação de comandos usando MediatR

### API Layer
- REST API com endpoints para:
  - Geração de faturas
  - Consulta de faturas
- Swagger UI para documentação

### Infrastructure Layer
- Persistência com PostgreSQL
- Mensageria com RabbitMQ
- Implementação de repositórios

## Fluxo de Processamento

1. **Requisição de Fatura**:   
   - Envia request para geração de fatura
   - Sistema valida dados e persiste informações básicas
   - Publica mensagem na fila RabbitMQ

2. **Processamento Assíncrono**:
   - Consumidor processa mensagem da fila
   - Gera documento de fatura (TXT/XML)
   - Atualiza registro com caminho do arquivo

3. **Consulta de Fatura**:
   - Cliente solicita fatura por ID
   - Sistema retorna documento processado

## Tecnologias Utilizadas

- .NET 8
- PostgreSQL
- RabbitMQ
- Entity Framework Core
- MediatR
- FluentValidation
- Swagger/OpenAPI

## Padrões Implementados

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- Repository Pattern
- Factory Pattern
- Value Objects
- Mediator Pattern
- Command Pattern

## Executando o Projeto

```bash
# Clone o repositório
git clone [url-do-repo]

# Configure as variáveis de ambiente no appsettings.json
- ConnectionStrings:DefaultConnection 
- RabbitMQ settings

# Execute as migrations
dotnet ef database update

# Inicie o RabbitMQ
docker run -d --hostname my-rabbit --name rabbit-mq -p 15672:15672 -p 5672:5672 rabbitmq:3-management

# Execute o projeto
dotnet run --project TheatricalPlayersRefactoring.Api
```

## Testes

- Testes unitários usando xUnit
- Mocks usando Moq

## Melhorias

- Implementar autenticação JWT(Adicionar autorização baseada em roles,Implementar refresh tokens)
- Implementar logging estruturado com Serilog
- Integrar com Elasticsearch e Kibana
- Monitoramento de saúde da API com Health Checks
- Implementar caching com Redis
- Desenvolver API Gateway
- Containerizar aplicação com Docker Compose
- Aumentar cobertura de testes (Testes de integração / end-to-end)
- Configurar CI/CD pipeline