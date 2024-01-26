using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    /// <summary>
    /// Classe que conté les dades de la llista
    /// </summary>
    public class Llista{


        [MaxLength(25)]
        public string? Nom { get; set; }

        [MaxLength(50)]
        public string? ID_MAC { get; set; }
        public ICollection<Song>? songs { get; set; } = new List<Song>();





    }
}