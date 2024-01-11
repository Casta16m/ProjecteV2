using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Canço{
        [Key]
        public string UID { get; set; }
        public ICollection<Format> Format { get; set; }

        public ICollection<conteAlbum> conteAlbum { get; set; }

    }
}