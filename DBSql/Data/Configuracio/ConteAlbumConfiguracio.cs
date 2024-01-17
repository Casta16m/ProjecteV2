using Microsoft.EntityFrameworkCore;
using ProjecteV2.ApiSql;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjecteV2.ApiSql{
    public class ConteAlbumConfiguracio : IEntityTypeConfiguration<ConteAlbum> {
        public void Configure(EntityTypeBuilder<ConteAlbum> builder){
            builder.HasKey(x => new { x.NomSong, x.data, x.NomAlbum });
            builder.HasOne(x => x.SongObj).WithMany(x => x.conteAlbum).HasForeignKey(x => x.NomSong);
            builder.HasOne(x => x.AlbumObj).WithMany(x => x.conteAlbum).HasForeignKey(x => new { x.NomAlbum, x.data, x.ArtistaNom });
        
            builder.HasIndex(x => x.NomAlbum).IsUnique(false);
            builder.HasIndex(x => x.data).IsUnique(false);
        }
    }
    
        
    }
