using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        /// <summary>
        /// Configuracio de l'album
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => new { x.NomAlbum, x.UIDSong, x.data });
            builder.HasOne(x => x.SongObj).WithMany(x => x.album).HasForeignKey(x => x.UIDSong);
        }
    }
}