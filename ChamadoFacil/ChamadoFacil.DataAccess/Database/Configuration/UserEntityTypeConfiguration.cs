using ChamadoFacil.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamadoFacil.DataAccess.Database.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Senha).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            builder.HasMany(x => x.Chamados).WithOne(x => x.Usuario).HasForeignKey(x => x.UsuarioId);
        }
    }
}