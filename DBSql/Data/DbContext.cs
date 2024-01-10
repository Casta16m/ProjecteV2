using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Format> Formats { get; set; }
        public DbSet<Artista> Artistes { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Canço> Cançons { get; set; }
        public DbSet<Es> es { get; set; }
        public DbSet<conteAlbum> conteAlbum { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration(new EsConfiguration());
            modelBuilder.ApplyConfiguration(new conteAlbumConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        }
    }

}
