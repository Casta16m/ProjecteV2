using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjecteV2.ApiMongoDB{
    public class Lletra{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _ID { get; set; } =  MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("OID")]
        public string AudioID { get; set; }

        [BsonElement("Lletra")]
        public string lletra { get; set; }
        
        public void SetCançoOID(Song Song){
            AudioID = Song._ID;
        }
    }
}