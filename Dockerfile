# ---- build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FilarmonicaMetais.sln", "."]
COPY ["src/FilarmonicaMetais.Api/FilarmonicaMetais.Api.csproj", "src/FilarmonicaMetais.Api/"]
COPY ["src/FilarmonicaMetais.Application/FilarmonicaMetais.Application.csproj", "src/FilarmonicaMetais.Application/"]
COPY ["src/FilarmonicaMetais.Domain/FilarmonicaMetais.Domain.csproj", "src/FilarmonicaMetais.Domain/"]
COPY ["src/FilarmonicaMetais.Infrastructure/FilarmonicaMetais.Infrastructure.csproj", "src/FilarmonicaMetais.Infrastructure/"]
COPY ["src/FilarmonicaMetais.Migrations.Postgres/FilarmonicaMetais.Migrations.Postgres.csproj", "src/FilarmonicaMetais.Migrations.Postgres/"]
COPY ["src/FilarmonicaMetais.Migrations.SqlServer/FilarmonicaMetais.Migrations.SqlServer.csproj", "src/FilarmonicaMetais.Migrations.SqlServer/"]

RUN dotnet restore "src/FilarmonicaMetais.Api/FilarmonicaMetais.Api.csproj"

COPY . .
WORKDIR /src/src/FilarmonicaMetais.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

# ---- runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN addgroup --system appgroup \
    && adduser --system --ingroup appgroup appuser \
    && mkdir -p /app/uploads \
    && chown -R appuser:appgroup /app

COPY --from=build /app/publish .
COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

# Fica como root aqui de propósito — o entrypoint precisa de privilégio pra corrigir
# a posse do volume montado em runtime antes de trocar pro usuário sem privilégios
# (appuser) pra rodar a aplicação de fato. Ver entrypoint.sh.
EXPOSE 8080

ENTRYPOINT ["/app/entrypoint.sh"]
