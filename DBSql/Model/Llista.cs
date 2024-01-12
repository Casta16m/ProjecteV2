using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Llista{
        [MaxLength(25)]
        public string? Dispositiu { get; set; }

        [MaxLength(25)]
        public string? Nom { get; set; }

        [MaxLength(50)]
        public string? ID_MAC { get; set; }
        public ICollection<Canço> cançons { get; set; }





    }
}