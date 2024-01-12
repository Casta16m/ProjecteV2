using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Grup{
        [Key]
        public string NomGrup { get; set; }
        public ICollection<Artista> artistes { get; set; }
        public ICollection<Participa> participa { get; set; }
    }
}