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

✅ Feito — **46 endpoints** registrados e verificados no Swagger:
- Domain: 13 entidades, 8 enums, 2 value objects (`Endereco`, `RedesSociais`)
- Infrastructure: `AppDbContext` + 13 configurations; 13 repositórios
  concretos + `UnitOfWork`; `JwtTokenService`, `BCryptPasswordHasher`,
  `CurrentUserService`, `LocalFileStorageService`; seleção de provider
  Postgres/SqlServer por configuração
- Migration `InitialCreate` gerada e validada (script SQL) em **Postgres e
  SQL Server**, a partir do mesmo modelo
- **Auth**: login, refresh, logout, me, change-password (exige senha atual)
- **Leitura pública**: banners ativos, instrumentos (com galeria), eventos
  (paginado), professores, cursos, depoimentos
- **Formulários públicos**: `POST` interessados, `POST` pedidos de apoio
- **CRUD administrativo** (`[Authorize]`), todos com endpoints reais:
  - Banners — inclui a lógica de reordenação (empurra em cadeia ao colidir
    com um `DisplayOrder` já ocupado, port do que foi implementado no painel)
  - Eventos, Professores, Cursos (valida que o `ProfessorId` existe),
    Depoimentos
  - Instrumentos — CRUD + endpoints dedicados para adicionar/remover foto
    da galeria (`POST`/`DELETE /fotos`)
  - Alunos — paginado com busca (nome/e-mail) e filtro de status, endpoint
    `PATCH /status` dedicado para arquivar/desarquivar
  - Interessados e Pedidos de Apoio — listagem + `PATCH /status`
  - **Doações e Usuários** — `[Authorize(Policy = "AdminOnly")]`, mesma
    restrição que o painel já aplica no front, agora também no servidor
  - Media — upload (`multipart/form-data`), listagem, checagem de uso
    (substitui `checkMediaUsage` do front, cruzando contra todas as
    entidades com campo de imagem) e exclusão (recusa se o arquivo estiver
    em uso)
- `Dockerfile` multi-stage + `entrypoint.sh` para Railway (volume de uploads,
  usuário sem privilégio)
- Solução compila limpa (`dotnet build`, 0 erros/avisos) e **foi testada de
  pé** duas vezes: `dotnet run` sobe, `/swagger/v1/swagger.json` responde 200
  e lista os 46 endpoints, `GET /api/depoimentos` percorre toda a cadeia
  (rota → controller → serviço → repositório → EF Core → tentativa real de
  conexão Postgres) e devolve erro estruturado pelo middleware em vez de
  stack trace cru quando o banco não está acessível

❌ Falta:
- Migração de dados do Supabase (ETL) — Fase 9 do plano
- Fase 0 (segurança) e Fase 1 (refactor das 53 chamadas diretas ao Supabase
  no front) — vivem no repositório `filarmonica-figma`, não neste
- Testes automatizados (unitários/integração) — nada foi escrito ainda
- Refresh token guardado só no banco (`Usuario.RefreshToken`), sem rotação
  por dispositivo — suficiente para o uso atual (poucos usuários admin/editor),
  mas vale revisar se o painel passar a ter múltiplas sessões simultâneas
  relevantes

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
