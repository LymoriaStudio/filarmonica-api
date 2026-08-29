# Filarmônica de Metais — API

Backend próprio (ASP.NET Core 8) para substituir gradualmente o Supabase do
projeto [filarmonica-figma](../filarmonica-figma). Arquitetura em camadas,
banco desacoplado via EF Core — Postgres hoje (Railway), SQL Server no futuro
(servidor do cliente), trocado só por configuração.

Plano completo de migração, decisões de modelo de dados e justificativas:
`filarmonica-figma/docs/PLANO-MIGRACAO-BACKEND.md`.

## Estrutura

```
FilarmonicaMetais.sln
├── Dockerfile
├── entrypoint.sh
├── .env.example
└── src/
    ├── FilarmonicaMetais.Domain/            Entidades, enums, value objects
    ├── FilarmonicaMetais.Application/       Interfaces, DTOs, casos de uso (*Service)
    ├── FilarmonicaMetais.Infrastructure/     EF Core, repositórios, JWT, storage
    ├── FilarmonicaMetais.Api/                Controllers ([ApiController])
    ├── FilarmonicaMetais.Migrations.Postgres/
    └── FilarmonicaMetais.Migrations.SqlServer/
```

## Estado atual

✅ Feito:
- Domain: 13 entidades, 8 enums, 2 value objects (`Endereco`, `RedesSociais`)
- Application: `IUnitOfWork` + 13 interfaces de repositório; casos de uso
  implementados: `AuthService` (login/refresh/logout/me/change-password),
  6 serviços de leitura pública (banners, instrumentos, eventos, professores,
  cursos, depoimentos), 2 serviços de formulário público (interessados,
  pedidos de apoio)
- Infrastructure: `AppDbContext` + 13 configurations; 13 repositórios
  concretos + `UnitOfWork`; `JwtTokenService`, `BCryptPasswordHasher`,
  `CurrentUserService`, `LocalFileStorageService`; seleção de provider
  Postgres/SqlServer por configuração
- Api: `Program.cs` completo (JWT Bearer, política `AdminOnly`, CORS,
  Swagger com cadeado só nas rotas `[Authorize]`, `ExceptionHandlingMiddleware`,
  migration + seed do admin automáticos no boot); 9 controllers com endpoints
  reais (`/api/auth/*`, `/api/banners/ativos`, `/api/instrumentos`,
  `/api/eventos`, `/api/professores`, `/api/cursos`, `/api/depoimentos`,
  `POST /api/interessados`, `POST /api/pedidos-apoio`)
- Migration `InitialCreate` gerada e validada (script SQL) em **Postgres e
  SQL Server**, a partir do mesmo modelo
- `Dockerfile` multi-stage + `entrypoint.sh` para Railway (volume de uploads,
  usuário sem privilégio)
- Solução compila limpa (`dotnet build`, 0 erros/avisos) e **foi testada de
  pé**: `dotnet run` sobe, `/swagger/v1/swagger.json` responde 200,
  `GET /api/depoimentos` percorre toda a cadeia (rota → controller → serviço
  → repositório → EF Core → tentativa real de conexão Postgres) e devolve
  erro estruturado pelo middleware em vez de stack trace cru

❌ Falta:
- Controllers de **CRUD administrativo** (`[Authorize]`) para banners,
  eventos, instrumentos, professores, cursos, depoimentos, alunos,
  interessados, pedidos de apoio, doações — hoje só existe leitura pública
  e os dois formulários de criação
- Endpoint de upload/listagem de `MediaAsset`
- Política `AdminOnly` aplicada a doações e usuários (a policy existe no
  `Program.cs`, falta usá-la nos controllers admin)
- Migração de dados do Supabase (ETL) — Fase 9 do plano
- Fase 0 (segurança) e Fase 1 (refactor das 53 chamadas diretas ao Supabase
  no front) — vivem no repositório `filarmonica-figma`, não neste

## Rodando localmente

```bash
dotnet build FilarmonicaMetais.sln
```

Copie `src/FilarmonicaMetais.Api/appsettings.Development.json.example` para
`appsettings.Development.json` (gitignored) e ajuste a connection string do
seu Postgres local. Depois:

```bash
cd src/FilarmonicaMetais.Api
dotnet run
```

O Swagger fica em `http://localhost:5080/swagger` (ajuste a porta conforme
`--urls` ou `launchSettings.json`).

Gerar/atualizar migrations (a partir da raiz do repo):

```bash
dotnet ef migrations add NomeDaMigration \
  --project src/FilarmonicaMetais.Migrations.Postgres/FilarmonicaMetais.Migrations.Postgres.csproj \
  --startup-project src/FilarmonicaMetais.Migrations.Postgres/FilarmonicaMetais.Migrations.Postgres.csproj \
  --output-dir Migrations
```

Troque `Postgres` por `SqlServer` para gerar a migration equivalente no outro provider.
