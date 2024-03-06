using Crawler.Models;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Data;

// Classe de contexto do banco de dados
public class LogContext : DbContext
{
    public DbSet<Log> LOGROBO { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=SQL9001.site4now.net;" +
            "Initial Catalog =db_aa5b20_apialmoxarifado_admin;" +
            "Password=master@123"); // "ConnectionString" string de conexão
    }
}
