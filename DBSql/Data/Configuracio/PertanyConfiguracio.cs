using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2{
    public class PertanyConfiguracio : IEntityTypeConfiguration<Pertany>
    {
        public void Configure(EntityTypeBuilder<Pertany> builder)
        {
            builder.HasKey(x => new { x.NomGrup, x.NomArtista });
            builder.HasOne(x => x.GrupObj).WithMany(x => x.Pertany).HasForeignKey(x => x.NomGrup);
            builder.HasOne(x => x.ArtistaObj).WithMany(x => x.Pertany).HasForeignKey(x => x.NomArtista);
            builder.HasIndex(x => x.NomGrup).IsUnique(false);
            builder.HasIndex(x => x.NomArtista).IsUnique(false);
        }
    }
}