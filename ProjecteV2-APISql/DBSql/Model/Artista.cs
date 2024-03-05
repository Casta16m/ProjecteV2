using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    /// <summary>
    /// Classe que conté les dades de l'artista
    /// </summary>
    public class Artista{
        [Key]
        [MaxLength(25)]
        public string NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        public ICollection<Grup>? Grups { get; set; } = new List<Grup>();
        public ICollection<Participa>? participa { get; set; }
    }
}