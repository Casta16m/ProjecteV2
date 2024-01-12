using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Canço{
        [Key]
        public string UID { get; set; }
        public ICollection<Extensio> extensio { get; set; }

        public ICollection<conteAlbum> conteAlbum { get; set; }
        public ICollection<ConteLlista> conteLlista { get; set; }
        public ICollection<Participa> participa { get; set; }
    }
}