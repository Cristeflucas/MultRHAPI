# MultRH API

Back-end do sistema de RH da **Mult Consultoria em RH** — plataforma para gestão de vagas, com controle de acesso por assinatura: usuários premium podem gerar a carta de encaminhamento em PDF das vagas, usuários comuns têm acesso apenas à visualização.

## Stack

- **.NET 8** / ASP.NET Core Web API
- **Entity Framework Core 8** + **Pomelo.EntityFrameworkCore.MySql** (MySQL)
- **ASP.NET Core Identity** + **JWT Bearer** para autenticação
- **AutoMapper** e **FluentValidation**
- **QuestPDF** para geração de documentos PDF
- **Swagger / Swashbuckle** para documentação da API
- **Serilog** para logging estruturado
- **xUnit** para testes automatizados

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
- Autorização por papel (`Admin` / `Candidate`) e por assinatura (`IsPremium`) via claims no token
- CRUD de vagas restrito a administradores
- Listagem e detalhe de vagas disponíveis para qualquer usuário autenticado
- Geração da carta de encaminhamento em PDF, restrita a usuários premium ou administradores
- Endpoint administrativo para liberar/revogar o acesso premium de um usuário
- Cadastro de planos de assinatura (`Plano`), com listagem aberta a qualquer autenticado e criação/edição restritas a administradores *(em desenvolvimento)*
- Rate limiting nos endpoints de registro/login, tratamento global de exceções (ProblemDetails) e logging estruturado com Serilog

## Como rodar localmente

### Pré-requisitos
- .NET 8 SDK
- MySQL Server

### Configuração
O projeto usa [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) para as credenciais de desenvolvimento — nenhum segredo fica no `appsettings.json`.

```bash
dotnet user-secrets set "SymmetricSecurityKey" "<sua-chave>" --project MultRHAPI/MultRHAPI.csproj
dotnet user-secrets set "ConnectionStrings:UserConnection" "Server=localhost;Database=MultRH;user=root;password=<sua-senha>;" --project MultRHAPI/MultRHAPI.csproj
```

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
| PATCH | `/api/User/{id}/premium` | Admin |
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

## Roadmap

- [x] Testes automatizados (unitários — validação de CPF e profiles do AutoMapper)
- [ ] Finalizar CRUD de planos de assinatura
- [ ] Integração de pagamento real (assinatura premium automática via gateway)
- [ ] Front-end em Blazor WebAssembly
- [ ] Testes de integração
