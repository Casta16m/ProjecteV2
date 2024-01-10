using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Es{

        public Canço? CançoObj { get; set; } 
        [Key]
        public string UID { get; set; }
        public Format? FormatObj { get; set; }
        [Key]
        public string ?Nom { get; set; }

    }
}