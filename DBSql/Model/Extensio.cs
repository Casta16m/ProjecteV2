using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProjecteV2.ApiSql{
    /// <summary>
    /// Classe que conté les dades de l'extensió
    /// </summary>
    public class Extensio{
        [Key]
        [MaxLength(10)]
        public string NomExtensio { get; set; }
        public ICollection<Song>? songs { get; set; } = new List<Song>();
    }
}