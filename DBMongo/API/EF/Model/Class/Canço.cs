using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProjecteV1.ApiMongoDB;
namespace ProjecteV1.ApiMongoDB{
    public class Canço{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _ID { get; set; } =  MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonElement("OID")]
        public string OID { get; set; }
        [BsonElement("AudioId")]
        public string AudioId { get; set; }
        
        public void SetAudio(Audio audio){
            AudioId = audio._ID;
        }


    }
}