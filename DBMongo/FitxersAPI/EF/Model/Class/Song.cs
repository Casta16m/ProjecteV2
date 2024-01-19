using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjecteV2.ApiMongoDB{
    public class Song{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; } =  MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        [BsonElement("UID")]
        public string? UID { get; set; }
        [BsonElement("AudioId")]
        public string? AudioId { get; set; }
        
        public void SetAudio(Audio audio){
            AudioId = audio._ID;
        }


    }
}