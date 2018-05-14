using ChamadoFacil.DataAccess.Database.Configuration;
using ChamadoFacil.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChamadoFacil.DataAccess.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=PE00Q1ME\\SQLEXPRESS;Initial Catalog=ChamadoFacil;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}