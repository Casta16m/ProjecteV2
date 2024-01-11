using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Extensio> Extensio { get; set; }
        public DbSet<Format> Format { get; set; }
        public DbSet<Artista> Artistes { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Canço> Cançons { get; set; }
        public DbSet<Pertany> Pertany { get; set; }
        public DbSet<conteAlbum> conteAlbum { get; set; }
        public DbSet<Grup> Grups { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration(new FormatConfiguracio());
            modelBuilder.ApplyConfiguration(new conteAlbumConfiguracio());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new PertanyConfiguracio());
        }
    }

}
