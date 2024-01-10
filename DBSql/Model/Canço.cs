using System.ComponentModel.DataAnnotations;

namespace ProjecteV2.ApiSql{
    public class Canço{
        [Key]
        public string UID { get; set; }
        public ICollection<Es> Es { get; set; } 
    }
}