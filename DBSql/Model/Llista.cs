using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Llista{
        
        public string? Dispositiu { get; set; }
    
        public string? Nom { get; set; }
       
        public string? ID_MAC { get; set; }
        public ICollection<ConteLlista> conteLlista { get; set; }




    }
}