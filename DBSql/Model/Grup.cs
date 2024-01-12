using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Grup{
        [Key]
        public string NomGrup { get; set; }
        public ICollection<Artista>? Artistas { get; set; }

    }
}