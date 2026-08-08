using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChamadoFacil.DataAccess.Database.Context
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ChamadoFacil;Trusted_Connection=True;Encrypt=False");
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
