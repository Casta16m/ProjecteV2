
using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Format{
        [Key]
        public string Nom { get; set; }
        public ICollection<Es> Es { get; set; }
    }
}