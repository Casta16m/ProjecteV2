using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Canço{
        [Key]
        public string UID { get; set; }
        
        [MaxLength(35)]
        public string NomCanço { get; set; }

        public ICollection<Album> albums { get; set; }
        public ICollection<Llista> llista { get; set; }
        public ICollection<Participa> participa { get; set; }
        public ICollection<Extensio> extensio { get; set; }
    }
}