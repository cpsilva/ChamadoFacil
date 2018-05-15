using ChamadoFacil.Models.Categoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamadoFacil.DataAccess.Database.Configuration
{
    public class CategoriaEntityTypeConfiguration : IEntityTypeConfiguration<CategoriaModel>
    {
        public void Configure(EntityTypeBuilder<CategoriaModel> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Descricao).IsRequired();

            builder.HasMany(x => x.Chamados).WithOne(x => x.Categoria).HasForeignKey(x => x.CategoriaId);
        }
    }
}