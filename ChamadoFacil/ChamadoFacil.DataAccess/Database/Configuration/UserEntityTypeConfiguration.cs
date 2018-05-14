using ChamadoFacil.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamadoFacil.DataAccess.Database.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Senha).IsRequired();
            builder.Property(x => x.Email).IsRequired();
        }
    }
}