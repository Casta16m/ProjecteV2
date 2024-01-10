using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjecteV2.ApiSql{
    public class conteAlbum{

        public Canço? CançoObj { get; set; }
        public string UID { get; set; }
        
        public Album? AlbumObj { get; set; }
        public string Nom { get; set; }
        public DateTime data { get; set; }
        public string ArtistaNom { get; set; }

    }
}