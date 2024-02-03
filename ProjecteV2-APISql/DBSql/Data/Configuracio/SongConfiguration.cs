using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ProjecteV2.ApiSql
{
     public class SongConfiguracio : IEntityTypeConfiguration<Song>
    {
        /// <summary>
        /// Configuracio de la canço
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasOne(x => x.SongObj).WithMany(x => x.songs).HasForeignKey(x => x.SongOriginal).OnDelete(DeleteBehavior.NoAction);
        
    }
}
}