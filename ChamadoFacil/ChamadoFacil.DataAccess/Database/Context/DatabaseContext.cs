using ChamadoFacil.DataAccess.Database.Configuration;
using ChamadoFacil.Models.Authentication;
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
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
        }
    }
}