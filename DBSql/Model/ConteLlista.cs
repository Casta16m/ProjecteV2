using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class ConteLlista{
        public Llista llistaObj { get; set; }
        public string NomLlista { get; set; }
        public string MAC { get; set; }

        public Canço? cançoObj { get; set; }
        public string UID { get; set; } 



       
    }


}