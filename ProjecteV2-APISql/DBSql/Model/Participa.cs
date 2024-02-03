using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    /// <summary>
    /// Classe que conté les dades de la participació
    /// </summary>
    public class Participa{

        public Song? SongObj { get; set; }

        public string? UID { get; set; } = new Guid().ToString();

        public Artista? ArtistaObj { get; set; }

        public string NomArtista { get; set; }

        public Grup? GrupObj { get; set; }

        public string NomGrup { get; set; }

        public Instrument? InstrumentObj { get; set; }

        public string NomInstrument { get; set; }
    }
}