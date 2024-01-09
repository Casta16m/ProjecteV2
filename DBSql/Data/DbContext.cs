using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Format> Formats { get; set; }
        public DbSet<Artista> Artistes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
        }
    }

}
