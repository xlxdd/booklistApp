using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace booklistInfrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "server=localhost;user=root;password=200212xlx;database=bookListApp";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));
            builder.UseMySql(connectionString, serverVersion);
            return new AppDbContext(builder.Options);
        }
    }
}
