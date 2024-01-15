using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Instrument{
        [Key]
        [MaxLength(25)]
        public string Nom { get; set; }
        [MaxLength(25)]
        public string Model { get; set; }

        public ICollection<Participa>? participa { get; set; }
    }
}