using ASPNETPlus.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ASPNETPlus.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("ASPNETPlus"));
            return new RepositoryContext(builder.Options);
        }
    }

}
