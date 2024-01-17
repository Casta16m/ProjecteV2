using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class ConteAlbum{
        public Song SongObj { get; set; }
        public string NomSong{ get; set; }
        
        public Album AlbumObj { get; set; }
        public string NomAlbum { get; set; }
        public DateTime data { get; set; }
        public string ArtistaNom { get; set; }
        public ICollection<Song> Songs { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}