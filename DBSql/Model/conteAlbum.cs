using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class conteAlbum{

        
        public Canço CançoObj { get; set; }

        public string UID { get; set; }
        
        public Album AlbumObj { get; set; }

        public string Nom { get; set; }

        public DateTime data { get; set; }

    }
}