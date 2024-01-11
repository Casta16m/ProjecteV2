using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Format{

        public Canço? CançoObj { get; set; } 
        [Key]
        public string UID { get; set; }
        public Extensio? ExtensioObj { get; set; }
        [Key]
        public string ?NomFormat { get; set; }

    }
}