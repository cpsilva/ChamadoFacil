using ChamadoFacil.DataAccess.Database.Configuration;
using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ChamadoFacil.DataAccess.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ChamadoModel> Chamados { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }

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