using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    /// <summary>
    /// Classe que conté les dades de la cançó
    /// </summary>
    public class Song{
        [Key]
        public string? UID { get; set; } = Guid.NewGuid().ToString();
        public DateTime? data { get; set; }
        
        [MaxLength(35)]
        public string NomSong { get; set; }

        public Song? SongObj { get; set; }
        public string? SongOriginal { get; set; }
        [MaxLength(35)]
        public string? Genere { get; set; }

        public ICollection<Album>? album { get; set; } = new List<Album>();
        public ICollection<Llista>? llista { get; set; }
        public ICollection<Participa>? participa { get; set; }
        public ICollection<Extensio>? extensio { get; set; } =  new List<Extensio>();
        public ICollection<Song>? songs { get; set; }
    }
}