using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProjecteV2.ApiSql{
    public class Extensio{
        [Key]
        public string NomExtensio { get; set; }
            public ICollection<Canço> cançons { get; set; }
    }
}