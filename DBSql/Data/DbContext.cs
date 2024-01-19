using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Extensio> Extensio { get; set; }
        public DbSet<Artista> Artistes { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Grup> Grups { get; set; }
        public DbSet<Llista> Llista { get; set; }
        public DbSet<Instrument> Instrument { get; set; }
        public DbSet<Participa> Participa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
    
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new LlistaConfiguracio());   
            modelBuilder.ApplyConfiguration(new ParticipaConfiguracio());
            modelBuilder.ApplyConfiguration(new SongConfiguracio());
        }
    }

}