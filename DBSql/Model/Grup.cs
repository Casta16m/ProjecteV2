using System.ComponentModel.DataAnnotations;

namespace ProjecteV2{
    public class Grup{
        [Key]
        public string Nom { get; set; }
        public ICollection<Pertany> Pertany { get; set; }
    }
}