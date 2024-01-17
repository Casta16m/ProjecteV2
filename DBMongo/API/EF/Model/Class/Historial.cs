using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ProjecteV2.ApiMongoDB{
    public class Historial{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; } =  MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        
        [BsonElement("MAC")]
        public string? MAC { get; set; }

        [BsonElement("Data")]
        public DateTime? Data { get; set; } = DateTime.Now;

        [BsonElement("OID")]
        public string? SongOID { get; set; }

        public void SetAudioOID(Song Song){
            SongOID = Song._ID;
        }
      
      
 }
}