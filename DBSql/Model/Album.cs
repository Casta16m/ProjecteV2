using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Album{
        [Key]
        public string Nom { get; set; }

        [Key]
        public DateTime data { get; set; }

        public string ArtistaNom { get; set; }

        public ICollection<conteAlbum> conteAlbum { get; set; }
    }
}