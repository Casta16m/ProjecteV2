using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Song{
        [Key]
        public string? UID { get; set; } = Guid.NewGuid().ToString();
        public DateTime? data { get; set; }
        
        [MaxLength(35)]
        public string NomSong { get; set; }

        public Song? SongObj { get; set; }
        public string? SongOriginal { get; set; }
        [MaxLength(35)]
        public string Genere { get; set; }

        public ICollection<Album>? album { get; set; }
        public ICollection<Llista>? llista { get; set; }
        public ICollection<Participa>? participa { get; set; }
        public ICollection<Extensio>? extensio { get; set; }
        public ICollection<Song>? songs { get; set; }
    }
}