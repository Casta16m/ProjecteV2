using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class conteAlbumConfiguracio : IEntityTypeConfiguration<conteAlbum>
    {
        public void Configure(EntityTypeBuilder<conteAlbum> builder)
        {
            builder.HasKey(x => new { x.UID, x.Nom, x.data });
            builder.HasOne(x => x.CançoObj).WithMany(x => x.conteAlbum).HasForeignKey(x => x.UID);
            builder.HasOne(x => x.AlbumObj).WithMany(x => x.conteAlbum).HasForeignKey(x => new { x.Nom, x.data, x.ArtistaNom });;
            builder.HasIndex(x => x.UID).IsUnique(false);
            builder.HasIndex(x => x.Nom).IsUnique(false);
            builder.HasIndex(x => x.data).IsUnique(false);
        }
    }
}
