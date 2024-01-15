using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Album{
        [MaxLength(25)]
        public string NomAlbum { get; set; }

        public DateTime? data { get; set; }
        
        [MaxLength(50)]
        public string ArtistaNom { get; set; }

        public ICollection<Song>? songs { get; set; }
    }
}