using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Artista{
        [Key]
        public string NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        public ICollection<Pertany> Pertany { get; set; }
        public ICollection<Participa> participa { get; set; }
    }
}