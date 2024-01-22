using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Album{
        [MaxLength(25)]
        public string NomAlbum { get; set; }

        public DateTime data { get; set; }
        
        public string? UIDSong { get; set; }
        public Song? SongObj { get; set; } 
    }
}