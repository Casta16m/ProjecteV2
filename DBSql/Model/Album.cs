using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Album{
        public string NomAlbum { get; set; }

        public DateTime data { get; set; }

        public string ArtistaNom { get; set; }

        public ICollection<conteAlbum> conteAlbum { get; set; }
    }
}