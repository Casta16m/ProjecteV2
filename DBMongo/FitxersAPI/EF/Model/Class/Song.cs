using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjecteV2.ApiMongoDB{
    /// <summary>
    /// Classe per a la col·lecció de songs
    /// </summary>
    public class Song{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; } =  MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        
        [BsonElement("UID")]
        public string? UID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId AudioFileId { get; set; }

    }
}