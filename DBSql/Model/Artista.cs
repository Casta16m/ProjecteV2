using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Artista{
        [Key]
        public string Nom { get; set; }
        public int AnyNaixement { get; set; }
    }
}