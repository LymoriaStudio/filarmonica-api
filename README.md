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
└── src/
    ├── FilarmonicaMetais.Domain/            Entidades, enums, value objects
    ├── FilarmonicaMetais.Application/       Interfaces de repositório/serviço, DTOs
    ├── FilarmonicaMetais.Infrastructure/     EF Core, AppDbContext, configurations
    ├── FilarmonicaMetais.Api/                Controllers ([ApiController])
    ├── FilarmonicaMetais.Migrations.Postgres/
    └── FilarmonicaMetais.Migrations.SqlServer/
```

## Estado atual

✅ Feito:
- Domain: 13 entidades, 8 enums, 2 value objects (`Endereco`, `RedesSociais`)
- Application: `IUnitOfWork` + 13 interfaces de repositório, interfaces de
  serviço (`IFileStorageService`, `IJwtTokenService`, `IPasswordHasher`,
  `ICurrentUserService`), exceptions, `PagedResult<T>`
- Infrastructure: `AppDbContext` + 13 `IEntityTypeConfiguration`
- Migration `InitialCreate` gerada e validada (script SQL) em **Postgres e
  SQL Server**, a partir do mesmo modelo — prova do desacoplamento de banco
- Solução compila limpa (`dotnet build`, 0 erros/avisos)

❌ Falta:
- Repositórios concretos + `UnitOfWork` (implementação)
- `JwtTokenService`, `BCryptPasswordHasher`, `CurrentUserService`,
  `LocalFileStorageService`
- `Program.cs` real (DI, JWT, CORS, Swagger)
- Controllers com endpoints
- `Dockerfile` + `entrypoint.sh` para o Railway
- Migração de dados do Supabase (ETL)

## Rodando localmente

```bash
dotnet build FilarmonicaMetais.sln
```

Gerar/atualizar migrations (a partir da raiz do repo):

```bash
dotnet ef migrations add NomeDaMigration \
  --project src/FilarmonicaMetais.Migrations.Postgres/FilarmonicaMetais.Migrations.Postgres.csproj \
  --startup-project src/FilarmonicaMetais.Migrations.Postgres/FilarmonicaMetais.Migrations.Postgres.csproj \
  --output-dir Migrations
```

Troque `Postgres` por `SqlServer` para gerar a migration equivalente no outro provider.
