using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Artista{
        [Key]
        public string NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        public ICollection<Grup> Grups { get; set; }
    }
}