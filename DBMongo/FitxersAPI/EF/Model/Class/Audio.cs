using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjecteV2.ApiMongoDB{
    public class Audio {
           
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; } =  MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonElement("AudioRepostiory")] 
        public byte[] AudioData { get; set; } = null!;
    }
 
}