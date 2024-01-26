using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    /// <summary>
    /// Classe que conté les dades de l'instrument
    /// </summary>
    public class Instrument{
        [Key]
        [MaxLength(25)]
        public string Nom { get; set; }
        [MaxLength(25)]
        public string Model { get; set; }

        public ICollection<Participa>? participa { get; set; }
    }
}