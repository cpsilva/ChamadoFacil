using ChamadoFacil.Models.Chamado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamadoFacil.DataAccess.Database.Configuration
{
    public class ChamadoEntityTypeConfiguration : IEntityTypeConfiguration<ChamadoModel>
    {
        public void Configure(EntityTypeBuilder<ChamadoModel> builder)
        {
            builder.ToTable("Chamados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Mensagem).IsRequired();
            builder.Property(x => x.Urgencia).IsRequired();

            builder.HasOne(x => x.Categoria).WithMany(x => x.Chamados).HasForeignKey(x => x.CategoriaId);
            builder.HasOne(x => x.Usuario).WithMany(x => x.Chamados).HasForeignKey(x => x.UsuarioId);
        }
    }
}