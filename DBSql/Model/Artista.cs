using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Artista{
        [Key]
        [MaxLength(25)]
        public string NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        public ICollection<Grup> Grups { get; set; }
        public ICollection<Participa> participa { get; set; }
    }
}