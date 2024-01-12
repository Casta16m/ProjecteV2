using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ProjecteV2.ApiSql
{
     public class SongConfiguracio : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasOne(x => x.SongObj).WithMany(x => x.songs).HasForeignKey(x => x.SongOriginal).OnDelete(DeleteBehavior.NoAction);
        
    }
}
}