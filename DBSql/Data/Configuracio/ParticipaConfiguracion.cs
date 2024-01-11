using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class ParticipaConfiguracio : IEntityTypeConfiguration<Participa>
    {
        public void Configure(EntityTypeBuilder<Participa> builder)
        {
            builder.HasKey(x => new { x.UID, x.NomCanço, x.NomArtista, x.NomGrup, x.NomInstrument });

            builder.HasOne({x => x.CançoObj}).WithMany({x => x.participa}).HasForeignKey({x => x.NomCanço});
            builder.HasOne({x => x.ArtistaObj}).WithMany({x => x.participa}).HasForeignKey({x => x.NomArtista});
            builder.HasOne({x => x.GrupObj}).WithMany({x => x.participa}).HasForeignKey({x => x.NomGrup});
            builder.HasOne({x => x.InstrumentObj}).WithMany({x => x.participa}).HasForeignKey({x => x.NomInstrument});

            builder.HasIndex(x => x.NomCanço).IsUnique(false);
            builder.HasIndex(x => x.NomArtista).IsUnique(false);
            builder.HasIndex(x => x.NomGrup).IsUnique(false);
            builder.HasIndex(x => x.NomInstrument).IsUnique(false);
        }
    }
}