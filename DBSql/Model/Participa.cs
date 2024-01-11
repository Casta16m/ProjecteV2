using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Participa{

        public Canço CançoObj { get; set; }

        public string UID { get; set; }

        public string NomCanço { get; set; }

        public Artista ArtistaObj { get; set; }

        public string NomArtista { get; set; }

        public Grup GrupObj { get; set; }

        public string? NomGrup { get; set; }

        public Instrument InstrumentObj { get; set; }

        public string? NomInstrument { get; set; }

        public ICollection<conteAlbum> conteAlbum { get; set; }
    }
}