using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Instrument{
        [Key]
        public string Nom { get; set; }
        public string Model { get; set; }

        public ICollection<Participa> participa { get; set; }
    }
}