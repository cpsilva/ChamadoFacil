using ChamadoFacil.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChamadoFacil.DataAccess.Database.Configuration
{
    public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TokenHash).IsRequired();
            builder.Property(x => x.ExpiresAt).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
