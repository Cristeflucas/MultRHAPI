# MultRH API

Back-end do sistema de RH da **Mult Consultoria em RH** — plataforma para gestão de vagas com controle de acesso por assinatura paga: usuários com assinatura ativa podem gerar a carta de encaminhamento em PDF das vagas, usuários comuns têm acesso apenas à visualização. A assinatura é ativada automaticamente após pagamento real via Mercado Pago.

## Stack

- **.NET 8** / ASP.NET Core Web API
- **Entity Framework Core 8** + **Pomelo.EntityFrameworkCore.MySql** (MySQL)
- **ASP.NET Core Identity** + **JWT Bearer** para autenticação
- **AutoMapper** e **FluentValidation**
- **QuestPDF** para geração de documentos PDF
- **Swagger / Swashbuckle** para documentação da API
- **Serilog** para logging estruturado
- **xUnit** para testes automatizados
- **Mercado Pago SDK (.NET)** para processamento de pagamentos

## Arquitetura

O projeto segue os princípios de **Clean Architecture**, organizado em 4 projetos com dependência unidirecional (`Domain ← Application ← Infrastructure ← Api`):

```
MultRHAPI.slnx
├── MultRHAPI/                    → Api (controllers, autenticação, Swagger)
└── src/
    ├── MultRH.Domain/            → Entidades e enums, sem dependências externas
    ├── MultRH.Application/       → DTOs, interfaces de serviço, validadores, profiles do AutoMapper
    └── MultRH.Infrastructure/    → EF Core, Identity, geração de PDF, implementações concretas
```

Isso mantém a regra de negócio isolada de detalhes técnicos (banco de dados, geração de PDF, framework web), facilitando testes e manutenção.

## Funcionalidades

- Cadastro e login de usuários (ASP.NET Core Identity + JWT), com Issuer/Audience validados e bloqueio de conta após tentativas de login falhas
- Autorização por papel (`Admin` / `Candidate`) e por assinatura ativa (`IsPremium`) via claims no token — calculada dinamicamente a cada login, nunca um campo fixo
- CRUD de vagas restrito a administradores; listagem e detalhe abertos a qualquer autenticado
- Geração da carta de encaminhamento em PDF, restrita a usuários com assinatura ativa ou administradores
- Cadastro de planos de assinatura (`Plano`): listagem aberta a qualquer autenticado, criação/edição restritas a administradores
- Controle de assinaturas (`Assinatura`): liga usuário a plano, com cálculo automático de vigência conforme a periodicidade do plano
- **Pagamento real via Mercado Pago** (Checkout Transparente): usuário paga com cartão, webhook confirma o pagamento (com validação de assinatura HMAC) e a assinatura é criada automaticamente — sem intervenção manual de Admin
- Rate limiting nos endpoints de registro/login, tratamento global de exceções (ProblemDetails) e logging estruturado com Serilog
- Usuário de banco de dados dedicado com privilégio mínimo (sem uso de `root` pela aplicação)

## Como rodar localmente

### Pré-requisitos
- .NET 8 SDK
- MySQL Server

### Configuração
O projeto usa [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) para as credenciais de desenvolvimento — nenhum segredo fica no `appsettings.json`.

```bash
dotnet user-secrets set "SymmetricSecurityKey" "<sua-chave>" --project MultRHAPI/MultRHAPI.csproj
dotnet user-secrets set "ConnectionStrings:UserConnection" "Server=localhost;Database=MultRH;user=<usuario-dedicado>;password=<sua-senha>;" --project MultRHAPI/MultRHAPI.csproj
dotnet user-secrets set "MercadoPago:AccessToken" "<seu-access-token-de-teste>" --project MultRHAPI/MultRHAPI.csproj
dotnet user-secrets set "MercadoPago:WebhookSecret" "<seu-webhook-secret>" --project MultRHAPI/MultRHAPI.csproj
```
As credenciais do Mercado Pago (Access Token, Public Key e Webhook Secret) são obtidas no [painel de desenvolvedores](https://www.mercadopago.com.br/developers/panel), na aba "Credenciais de teste" — a aplicação precisa estar configurada para a **API Pagamentos** (não "API Orders") para que o Checkout Transparente funcione corretamente.

Para testar webhooks localmente, é necessário expor a API com um túnel público (ex: [ngrok](https://ngrok.com/)) e cadastrar a URL gerada em Webhooks → Configurar notificação, no painel do Mercado Pago.

### Banco de dados
```bash
dotnet ef database update --project src/MultRH.Infrastructure --startup-project MultRHAPI
```

### Executando
```bash
dotnet run --project MultRHAPI
```
A documentação interativa fica disponível em `/swagger` (ambiente de desenvolvimento).

## Principais endpoints

| Método | Rota | Acesso |
|---|---|---|
| POST | `/api/User/register` | Público |
| POST | `/api/User/login` | Público |
| GET | `/api/Vaga` | Autenticado |
| GET | `/api/Vaga/{id}` | Autenticado |
| GET | `/api/Vaga/{id}/pdf` | Premium ou Admin |
| POST | `/api/Vaga` | Admin |
| PUT | `/api/Vaga/{id}` | Admin |
| DELETE | `/api/Vaga/{id}` | Admin |
| GET | `/api/Plano` | Autenticado |
| GET | `/api/Plano/{id}` | Autenticado |
| POST | `/api/Plano` | Admin |
| PATCH | `/api/Plano/{id}` | Admin |
| GET | `/api/Assinatura` | Autenticado (assinatura ativa do próprio usuário) |
| POST | `/api/Assinatura` | Admin |
| PATCH | `/api/Assinatura/{id}/cancelar` | Admin |
| POST | `/api/Pagamento` | Autenticado |
| POST | `/api/Pagamento/webhook` | Público (validado por assinatura HMAC do Mercado Pago) |

## Roadmap

- [x] Testes automatizados (unitários — validação de CPF e profiles do AutoMapper)
- [x] CRUD de planos de assinatura
- [x] Integração de pagamento real (Mercado Pago — pagamento, webhook e assinatura automática)
- [ ] Front-end em Blazor WebAssembly
- [ ] Testes de integração
- [ ] Docker e CI
