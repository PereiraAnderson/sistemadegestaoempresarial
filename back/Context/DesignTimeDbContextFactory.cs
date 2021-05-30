using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SGE.Context
{
    public class DesignTimeDbContextFactory :
        IDesignTimeDbContextFactory<SGEDbContext>
    {
        public SGEDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Migration.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SGEDbContext>();

            var connectionString = configuration.GetConnectionString("SqlServerSGE");

            builder.UseSqlServer(connectionString);

            return new SGEDbContext(builder.Options);
        }
    }
}
