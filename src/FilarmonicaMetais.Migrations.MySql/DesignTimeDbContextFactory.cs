using FilarmonicaMetais.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FilarmonicaMetais.Migrations.MySql;

// Usado só pela CLI (`dotnet ef migrations add` / `database update`) para saber
// como construir o AppDbContext neste projeto. A connection string aqui é só um
// placeholder de design-time — em runtime real, quem decide é
// FilarmonicaMetais.Infrastructure.DependencyInjection, a partir da configuração da Api.
//
// ServerVersion fixo (em vez de AutoDetect) porque migrations add/update em
// design-time não pode depender de um MySQL de verdade estar no ar — o alvo
// real do cliente é MySQL 9.7.x, mas o dialect gerado pelo Pomelo pra 8.0 já
// cobre a sintaxe usada neste schema.
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(
            "Server=localhost;Database=filarmonica_designtime;User=root;Password=root;",
            new MySqlServerVersion(new Version(8, 0, 0)),
            mysql => mysql.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.FullName));

        return new AppDbContext(optionsBuilder.Options);
    }
}
